

using Application.DTOs;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper
{
	[Mapper]
	public partial class TicketTypeMapper
	{
		public partial TicketType ToEntity(CreateTicketTypeDto dto);
		public partial TicketTypeDto ToDto(TicketType Entity);

		public partial void UpdateEntity(UpdateTicketTypeDto dto, TicketType entity);


	}
}
