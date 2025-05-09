using Application.DTOs;
using Domain.Response;
using Infrastructure.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

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

		[HttpGet("GetAll")]
		public async Task<ActionResult<UserDto>> GetAll()
		{
			var users = await Reposatory.GetAll();

			return users is not null ? Ok(users) : BadRequest("No Data Found");
		}
	}
}
