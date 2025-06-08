using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class VirtualRoomConfiguration : IEntityTypeConfiguration<VirtualRoom>
	{
		public void Configure(EntityTypeBuilder<VirtualRoom> builder)
		{
			builder.ToTable("VirtualRooms");

	
			builder.HasKey(vr => vr.Id);

	
			builder.HasOne(vr => vr.Session) 
				.WithOne(s => s.VirtualRoom) 
				.HasForeignKey<VirtualRoom>(vr => vr.SessionId) 
				.OnDelete(DeleteBehavior.Cascade); 

		
			builder.Property(vr => vr.Name)
				.IsRequired()
				.HasMaxLength(200); 

			builder.Property(vr => vr.Platform)
				.IsRequired() 
				.HasMaxLength(100); 

			builder.Property(vr => vr.AccessCode)
				.IsRequired() 
				.HasMaxLength(50); 
		}
	}
}
