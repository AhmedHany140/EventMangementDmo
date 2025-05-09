

using Application.DTOs;
using Application.Mapper;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Provider;
using Infrastructure.Reposatory;
using Microsoft.AspNetCore.Mvc;

namespace MeetingEventsAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChatMessageController(IService<ChatMessage> repo,ChatMessageMapper mapper) : GlobalController<CreateChatMessageDto, ChatMessageDto, UpdateChatMessageDto>
	{
		private readonly IService<ChatMessage> Reposatory= repo;
		private readonly ChatMessageMapper mapper = mapper;
		public override async Task<ActionResult<Response>> Create(CreateChatMessageDto dto)
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

		public override async Task<ActionResult<IEnumerable<ChatMessageDto>>> GetAll()
		{
			var list = await Reposatory.GetAllAsync(new string[] { "User" });

			List<ChatMessageDto> Dtos =
				list.Select(e => mapper.ToDto(e)).ToList();

			return Dtos is null ? BadRequest("Not Found") : Ok(Dtos);
		}

		public override async Task<ActionResult<ChatMessageDto>> GetById(int id)
		{
			var Entity = await Reposatory.GetAsync(new string[]{  "User"  }, id);

			

			var Dto = mapper.ToDto(Entity);

			return Dto is null ? BadRequest("Not Found") : Ok(Dto);
		}

		public override async Task<ActionResult<Response>> Update(UpdateChatMessageDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest("Invailed DATA");

			var Entity = await Reposatory.GetAsync(new string[] { nameof(AppUser) }, dto.Id);

			mapper.UpdateEntity(dto, Entity);

			var Response = await Reposatory.UpdateAsync(Entity);

			return Response.Success ? Ok(Response) : BadRequest(Response);
		}
	}
}
