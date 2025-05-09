using Application.DTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingEventsAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SessionSpeakerController(IProvider<SessionSpeakerDto,SessionSpeakerDetails> repo) : ControllerBase
	{
		private readonly IProvider<SessionSpeakerDto, SessionSpeakerDetails> Reposatory = repo;



		[HttpPost]
		public async Task<ActionResult<Response>> Create(SessionSpeakerDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest("Invaild data");

			var Response = await Reposatory.Create(dto);

			return Response.Success ? Ok(Response) : BadRequest(Response);
		}

		[HttpGet]
		public async Task<ActionResult<SessionSpeakerDetails>> GetAll()
		{
			var Result = await Reposatory.GetDetails();


			return Result is not null ? Ok(Result) :
				   BadRequest("Not Found");
		}
	}
}
