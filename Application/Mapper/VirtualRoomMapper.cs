

using Application.DTOs;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper
{
	[Mapper]
	public partial class VirtualRoomMapper
	{
		public partial VirtualRoom ToEntity(CreateVirtualRoomDto dto);
		public partial VirtualRoomDto ToDto(VirtualRoom Entity);

		public partial void UpdateEntity(UpdateVirtualRoomDto dto, VirtualRoom entity);



	}
}
