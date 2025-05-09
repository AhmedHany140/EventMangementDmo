
using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper
{
	[Mapper]
	public partial class RegistrationsMapper
	{
		public partial Registrations ToEntity(CreateRegistrationDto dto);

		[MapProperty(nameof(Registrations.Attendance.UserName), nameof(RegistrationDto.AttendanceName))]
		[MapProperty(nameof(Registrations.TicketType.Name), nameof(RegistrationDto.TicketTypeName))]

		public partial RegistrationDto ToDto(Registrations Entity);

		public partial void UpdateEntity(UpdateRegistrationDto dto, Registrations entity);

	}


}
