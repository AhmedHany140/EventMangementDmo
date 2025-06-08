using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class SessionSpeakerConfiguration : IEntityTypeConfiguration<SessionSpeaker>
	{
		public void Configure(EntityTypeBuilder<SessionSpeaker> builder)
		{
			builder.ToTable("SessionSpeakers");

			
			builder.HasKey(ss => new { ss.SessionId, ss.SpeakerId }); 

		
			builder.HasOne(ss => ss.Session) 
				.WithMany(s => s.SessionSpeakers) 
				.HasForeignKey(ss => ss.SessionId) 
				.OnDelete(DeleteBehavior.Cascade); 

			builder.HasOne(ss => ss.Speaker) 
				.WithMany(u => u.SessionSpeakers) 
				.HasForeignKey(ss => ss.SpeakerId) 
				.OnDelete(DeleteBehavior.Cascade); 

		
			builder.Property(ss => ss.Role)
				.IsRequired() 
				.HasMaxLength(100);
		}
	}
}
