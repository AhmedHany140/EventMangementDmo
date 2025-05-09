
using Application.DTOs;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper
{
	[Mapper]
	public partial class EventMapper
	{
		public partial Event ToEntity(CreateEventDto dto);
		public partial EventDto ToDto(Event Entity);

		public partial void UpdateEntity(UpdateEventDto dto, Event entity);



	}
}
