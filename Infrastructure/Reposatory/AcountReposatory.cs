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
using System.Security.Cryptography;
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
				return new Response("Invalid data sent");

			var userfromDb = await userManager.FindByEmailAsync(dto.Email);

			if (userfromDb is null)
				return new Response("User not found");

			var correctpassword = await userManager.CheckPasswordAsync(userfromDb, dto.Password);

			if (!correctpassword)
				return new Response("Incorrect password");

			// Create Access Token
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));
			var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>
	{
		new Claim(ClaimTypes.NameIdentifier, userfromDb.Id),
		new Claim(ClaimTypes.Name, userfromDb.UserName!)
	};

			var roles = await userManager.GetRolesAsync(userfromDb);
			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

			var accessToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
				issuer: configuration["JWT:issuer"],
				audience: configuration["JWT:audience"],
				expires: DateTime.UtcNow.AddMinutes(15),
				claims: claims,
				signingCredentials: signingCredentials
			));

			// Create Refresh Token
			string refreshToken =await GenerateRefreshToken();
			userfromDb.RefreshToken = refreshToken;
			userfromDb.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
			await userManager.UpdateAsync(userfromDb);

			// Return both tokens
			return new Response($"Access Token = {accessToken} \n" +
				$"Refresh Token = {refreshToken}"
			, true);
		}


		private async Task< string> GenerateRefreshToken()
		{
			var randomBytes = new byte[64];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomBytes);
			return Convert.ToBase64String(randomBytes);
		}



		public async Task<Response> RefreshTokenAsync(TokenRequest request)
		{
			if (request == null)
				return new Response("Invalid request");

			var principal = GetPrincipalFromExpiredToken(request.AccessToken);
			if (principal == null)
				return new Response("Invalid access token");

			var username = principal.Identity?.Name;
			var user = await userManager.FindByNameAsync(username);

			if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
				return new Response("Unauthorized");

			// generate new tokens
			var newAccessToken = CreateAccessToken(principal.Claims.ToList());
			var newRefreshToken = await GenerateRefreshToken();

			// update in DB
			user.RefreshToken = newRefreshToken;
			user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
			await userManager.UpdateAsync(user);

			return new Response($"Access Token = {newAccessToken} \n " +
				$"Refresh Token = {newRefreshToken}",
			 true);
		}


		private string CreateAccessToken(List<Claim> claims)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));
			var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: configuration["JWT:issuer"],
				audience: configuration["JWT:audience"],
				expires: DateTime.UtcNow.AddMinutes(15),
				claims: claims,
				signingCredentials: signingCredentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}


		private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = false,
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!)),
				ValidateLifetime = false 
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

			if (securityToken is not JwtSecurityToken jwtSecurityToken ||
				!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException("Invalid token");

			return principal;
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

		public async Task<UserDto> GetById(string id)
		{
			var user = await userManager.FindByIdAsync(id);


			var userdto = new UserDto(user.UserName, user.Email, user.PhoneNumber);

			return userdto;
		}



	}
}
