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
	public class EventController(EventMapper mapper, IService<Event> reposatory) : GlobalController<CreateEventDto, EventDto, UpdateEventDto>
	{
		private readonly EventMapper mapper=mapper;
		private readonly IService<Event> reposatory=reposatory;

		public override async Task<ActionResult<Response>> Create(CreateEventDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest("Invailed DATA");

			var Entity = mapper.ToEntity(dto);



			var Response = await reposatory.AddAsync(Entity);

			return Response.Success ? Ok(Response) : BadRequest(Response);
		}

		public override async Task<ActionResult<Response>> Delete(int id)
		{
			var Response = await reposatory.DeleteAsync(id);

			return Response.Success ? Ok(Response) : BadRequest(Response);
		}

		public override async Task<ActionResult<IEnumerable<EventDto>>> GetAll()
		{
			var list = await reposatory.GetAllAsync(null);

			List<EventDto> Dtos =
				list.Select(e => mapper.ToDto(e)).ToList();

			return Dtos is null ? BadRequest("Not Found") : Ok(Dtos);
		}

		public override async Task<ActionResult<EventDto>> GetById(int id)
		{
			var Entity = await reposatory.GetAsync(null,id);

			var Dto = mapper.ToDto(Entity);

			return Dto is null ? BadRequest("Not Found") : Ok(Dto);
		}

		public  override async Task<ActionResult<Response>> Update(UpdateEventDto dto)
		{

			if (!ModelState.IsValid)
				return BadRequest("Invailed DATA");

			var Entity = await reposatory.GetAsync(null, dto.Id);

			mapper.UpdateEntity(dto, Entity);

			var Response = await reposatory.UpdateAsync(Entity);

			return Response.Success ? Ok(Response) : BadRequest(Response);
		}
	}




}
