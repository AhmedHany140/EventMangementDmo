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
	public class RegisterationController(IService<Registrations> service,RegistrationsMapper mapper) : 
		GlobalController<CreateRegistrationDto,RegistrationDto,UpdateRegistrationDto>
	{
		private readonly IService<Registrations> Reposatory = service;
		private readonly RegistrationsMapper mapper = mapper;

		public override async Task<ActionResult<Response>> Create(CreateRegistrationDto dto)
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

		public override async Task<ActionResult<IEnumerable<RegistrationDto>>> GetAll()
		{
			var list = await Reposatory.GetAllAsync(new string[] {"Attendance",nameof(Event),nameof(TicketType)});

			List<RegistrationDto> Dtos =
				list.Select(e => mapper.ToDto(e)).ToList();

			return Dtos is null ? BadRequest("Not Found") : Ok(Dtos);
		}

		public override async Task<ActionResult<RegistrationDto>> GetById(int id)
		{
			var Entity = await Reposatory.GetAsync(new string[] { "Attendance", nameof(Event), nameof(TicketType) }, id);

			var Dto = mapper.ToDto(Entity);

			return Dto is null ? BadRequest("Not Found") : Ok(Dto);
		}

		public override async Task<ActionResult<Response>> Update(UpdateRegistrationDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest("Invailed DATA");

			var Entity = await Reposatory.GetAsync(new string[] { nameof(AppUser), nameof(Event), nameof(TicketType) }, dto.Id);

			mapper.UpdateEntity(dto, Entity);

			var Response = await Reposatory.UpdateAsync(Entity);

			return Response.Success ? Ok(Response) : BadRequest(Response);
		}
	}
}
