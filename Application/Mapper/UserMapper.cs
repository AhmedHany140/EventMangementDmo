
using Application.DTOs;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper
{
	[Mapper]
	public partial class UserMapper
	{
		public partial AppUser ToEntity(RegisterDTO dto);
	

	}
}
