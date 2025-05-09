using Application.DTOs;
using Application.Mapper;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingEventsAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VirtualRoomController(IService<VirtualRoom> repo, VirtualRoomMapper mapper) :
		GlobalController<CreateVirtualRoomDto, VirtualRoomDto, UpdateVirtualRoomDto>
	{
		private readonly IService<VirtualRoom> Reposatory = repo;
		private readonly VirtualRoomMapper mapper = mapper;

		public override async Task<ActionResult<Response>> Create(CreateVirtualRoomDto dto)
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

		public override async Task<ActionResult<IEnumerable<VirtualRoomDto>>> GetAll()
		{
			var list = await Reposatory.GetAllAsync(null);

			List<VirtualRoomDto> Dtos =
				list.Select(e => mapper.ToDto(e)).ToList();

			return Dtos is null ? BadRequest("Not Found") : Ok(Dtos);
		}

		public override async Task<ActionResult<VirtualRoomDto>> GetById(int id)
		{
			var Entity = await Reposatory.GetAsync(null,id);

			var Dto = mapper.ToDto(Entity);

			return Dto is null ? BadRequest("Not Found") : Ok(Dto);
		}

		public override async Task<ActionResult<Response>> Update(UpdateVirtualRoomDto dto)
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
