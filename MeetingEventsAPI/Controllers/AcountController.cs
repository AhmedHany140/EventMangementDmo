using Application.DTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Provider;
using Infrastructure.Reposatory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeetingEventsAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AcountController(Iuser repo) : ControllerBase
	{
		private readonly Iuser Reposatory = repo;

		[HttpPost("Register")]
		public async Task<ActionResult<Response>> Register(RegisterDTO register)
		{
			if (!ModelState.IsValid)
				return BadRequest("Not Found This Acount");


			var Response = await Reposatory.Register(register);

			return Response.Success ? Ok(Response) : BadRequest(Response);
		}

		[HttpPost("Login")]
		public async Task<ActionResult<Response>> Login(LoginDTO login)
		{
			if (!ModelState.IsValid)
				return BadRequest("Not Found This Acount");

			var Response = await Reposatory.Login(login);

			return Response.Success ? Ok(Response) : BadRequest(Response);
		}


		[HttpPost("refresh")]
		public async Task<IActionResult> RefreshToken([FromBody] TokenRequest request)
		{
			var result = await Reposatory.RefreshTokenAsync(request);
			if (!result.Success)
				return Unauthorized(result.message);

			return Ok(result);
		}


		[HttpGet("GetAll")]
		public async Task<ActionResult<UserDto>> GetAll()
		{
			var users = await Reposatory.GetAll();

			return users is not null ? Ok(users) : BadRequest("No Data Found");
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<UserDto>> GetById(string id)
		{
			var users = await Reposatory.GetById(id);

			return users is not null ? Ok(users) : BadRequest("No Data Found");
		}

	}
}
