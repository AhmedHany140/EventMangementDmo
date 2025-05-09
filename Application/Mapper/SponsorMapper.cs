

using Application.DTOs;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper
{
	[Mapper]
	public partial class SponsorMapper
	{
		public partial Sponsor ToEntity(CreateSponsorDto dto);
		public partial SponsorDto ToDto(Sponsor Entity);

		public partial void UpdateEntity(UpdateSponsorDto dto, Sponsor entity);


	}
}
