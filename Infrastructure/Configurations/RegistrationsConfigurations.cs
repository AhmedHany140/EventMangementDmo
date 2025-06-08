using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class RegistrationsConfiguration : IEntityTypeConfiguration<Registrations>
	{
		public void Configure(EntityTypeBuilder<Registrations> builder)
		{
			builder.ToTable("Registrations");

			builder.HasKey(r => r.Id);

	
			builder.HasOne(r => r.Attendance)
				.WithMany(x=>x.Registrations) 
				.HasForeignKey(r => r.AttendanceId) 
				.OnDelete(DeleteBehavior.Cascade); 

			builder.HasOne(r => r.Event) 
				.WithMany(x=>x.Registrations) 
				.HasForeignKey(r => r.EventId) 
				.OnDelete(DeleteBehavior.Cascade); 

			builder.HasOne(r => r.TicketType) 
				.WithMany(x=>x.Registrations) 
				.HasForeignKey(r => r.TicketTypeId) 
				.OnDelete(DeleteBehavior.Restrict); 

		
			builder.Property(r => r.RegistrationDate)
				.IsRequired();

			builder.Property(r => r.CheckInStatus)
				.IsRequired();

			builder.Property(r => r.ChickInTime);
			
		}
	}
}
