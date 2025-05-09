using Application.DTOs;
using Application.Mapper;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Provider;
using Infrastructure.Reposatory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingEventsAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SessionController(IService<Session> repo,SessionMapper mapper) : GlobalController<CreateSessionDto, SessionDto, UpdateSessionDto>
	{
		private readonly IService<Session> Reposatory = repo;
		private readonly SessionMapper mapper = mapper;
		public override async Task<ActionResult<Response>> Create(CreateSessionDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest("Invailed DATA");

			var Entity = mapper.ToEntity(dto);


			var Response = await Reposatory.AddAsync(Entity);

			return Response.Success ? Ok(Response) : BadRequest(Response);
		}

		public override async Task<ActionResult<Response>> Delete(int id)
		{
			var Response = await Reposatory.DeleteAsync(id);

			return Response.Success ? Ok(Response) : BadRequest(Response);

		}

		public override async Task<ActionResult<IEnumerable<SessionDto>>> GetAll()
		{
			var list = await Reposatory.GetAllAsync(new string[] {nameof(Event),nameof(VirtualRoom)});

			List<SessionDto> Dtos =
				list.Select(e => mapper.ToDto(e)).ToList();

			return Dtos is null ? BadRequest("Not Found") : Ok(Dtos);
		}

		public override async Task<ActionResult<SessionDto>> GetById(int id)
		{
			var Entity = await Reposatory.GetAsync(new string[] { nameof(Event), nameof(VirtualRoom) }, id);

			var Dto = mapper.ToDto(Entity);

			return Dto is null ? BadRequest("Not Found") : Ok(Dto);
		}

		public override async Task<ActionResult<Response>> Update(UpdateSessionDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest("Invailed DATA");

			var Entity = await Reposatory.GetAsync(new string[] { nameof(Event), nameof(VirtualRoom) }, dto.Id);

			mapper.UpdateEntity(dto, Entity);

			var Response = await Reposatory.UpdateAsync(Entity);

			return Response.Success ? Ok(Response) : BadRequest(Response);
		}
	}
}
