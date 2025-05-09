

using Application.DTOs;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper
{
	[Mapper]
	public partial class SessionMapper
	{
		public partial Session ToEntity(CreateSessionDto dto);
		public partial SessionDto ToDto(Session Entity);

		public partial void UpdateEntity(UpdateSessionDto dto, Session entity);


	}
}
