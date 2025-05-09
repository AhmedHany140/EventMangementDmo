using Application.DTOs;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper
{
	[Mapper]
	public partial class ResourceMapper
	{
		public partial Resource ToEntity(CreateResourceDto dto);

		[MapProperty(nameof(Resource.Event.Title), nameof(ResourceDto.EventTitle))]
		public partial ResourceDto ToDto(Resource Entity);

		public partial void UpdateEntity(UpdateResourceDto dto, Resource entity);


	}
}
