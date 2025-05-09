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
	public class PollController(IService<Poll> service,PollMapper mapper) : GlobalController<CreatePollDto,PollDto,UpdatePollDto>
	{
		private readonly IService<Poll> Reposatory = service;
		private readonly PollMapper mapper = mapper;

		public override async Task<ActionResult<Response>> Create(CreatePollDto dto)
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

		public override async Task<ActionResult<IEnumerable<PollDto>>> GetAll()
		{
			var list = await Reposatory.GetAllAsync(null);

			List<PollDto> Dtos =
				list.Select(e => mapper.ToDto(e)).ToList();

			return Dtos is null ? BadRequest("Not Found") : Ok(Dtos);
		}

		public override async Task<ActionResult<PollDto>> GetById(int id)
		{
			var Entity = await Reposatory.GetAsync(null,id);

			var Dto = mapper.ToDto(Entity);

			return Dto is null ? BadRequest("Not Found") : Ok(Dto);
		}

		public override async Task<ActionResult<Response>> Update(UpdatePollDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest("Invailed DATA");

			var Entity = await Reposatory.GetAsync(null,dto.Id);

			mapper.UpdateEntity(dto, Entity);

			var Response = await Reposatory.UpdateAsync(Entity);

			return Response.Success ? Ok(Response) : BadRequest(Response);
		}
	}
}
