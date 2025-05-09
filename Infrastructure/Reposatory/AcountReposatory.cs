using Application.DTOs;
using Application.Mapper;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Provider;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Reposatory
{
	public class AcountReposatory(UserManager<AppUser> manager,IConfiguration configuration,UserMapper mapper): Iuser
	{
		private readonly UserManager<AppUser> userManager = manager;
		private readonly IConfiguration configuration=configuration;
		private readonly UserMapper userMapper = mapper;

		public async Task<Response> Login(LoginDTO dto)
		{
			if (dto is null)
				return new Response("invalaid Data send");

			var userfromDb = await userManager.FindByEmailAsync(dto.Email);

			if (userfromDb is null)
				return new Response("User Not Found");

			var correctpassword = await userManager.CheckPasswordAsync(userfromDb, dto.Password);

			if (!correctpassword)
				return new Response(" Uncorrect Password");

			SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));


			SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			List<Claim> claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.NameIdentifier, userfromDb.Id));
			claims.Add(new Claim(ClaimTypes.Name, userfromDb.UserName!));


			IList<string> roles = await userManager.GetRolesAsync(userfromDb);

			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


			JwtSecurityToken securityToken = new JwtSecurityToken(
				issuer: configuration["JWT:issuer"],
				audience: configuration["JWT:audience"],
				expires: DateTime.UtcNow.AddDays(30),
				claims: claims,
				signingCredentials: signingCredentials
				);


			var token = new JwtSecurityTokenHandler().WriteToken(securityToken);



			return new Response(token, true);
		}

		public async Task<Response> Register(RegisterDTO dto)
		{
			if (dto is null)
				return new Response("Invalid Data", false);

			var NewUser = userMapper.ToEntity(dto);

			var result = await userManager.CreateAsync(NewUser, dto.Password);

			if (!result.Succeeded)
				return new Response("Can not Create Acount");

			return new Response("Acount Created Successfully", true);
		}


		public async Task<IEnumerable<UserDto>> GetAll()
		{
			var users = await userManager.Users.ToListAsync();


			var userdto = users.Select(u => new UserDto(u.UserName, u.Email, u.PhoneNumber));

			return userdto;
		}
	}
}
