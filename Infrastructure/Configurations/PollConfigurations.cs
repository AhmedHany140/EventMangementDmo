using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class PollConfiguration : IEntityTypeConfiguration<Poll>
	{
		public void Configure(EntityTypeBuilder<Poll> builder)
		{
			builder.ToTable("Polls");

		
			builder.HasKey(p => p.Id);

			
			builder.Property(p => p.Question)
				.IsRequired()
				.HasMaxLength(500); 

			builder.Property(p => p.IsAnonymous)
				.IsRequired();

			builder.Property(p => p.CreatedAt)
				.IsRequired()
				.HasDefaultValueSql("GETDATE()"); 

			builder.Property(p => p.PollType)
				.IsRequired() 
				.HasConversion<string>(); 

		
			builder.HasOne(p => p.Session)
				.WithMany(s => s.Polls)
				.HasForeignKey(p => p.SessionId)
				.OnDelete(DeleteBehavior.Cascade);




			
		}
	}
}
