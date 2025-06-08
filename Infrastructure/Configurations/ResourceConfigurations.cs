using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
	{
		public void Configure(EntityTypeBuilder<Resource> builder)
		{
			builder.ToTable("Resources");

		
			builder.HasKey(r => r.Id);

		
			builder.HasOne(r => r.Session) 
				.WithMany(s => s.Resources) 
				.HasForeignKey(r => r.SessionId)
				.OnDelete(DeleteBehavior.Cascade); 

			builder.HasOne(r => r.Event) 
				.WithMany(e => e.Resource) 
				.HasForeignKey(r => r.EventId) 
				.OnDelete(DeleteBehavior.Cascade); 

			
			builder.Property(r => r.Name)
				.IsRequired() 
				.HasMaxLength(255); 

			builder.Property(r => r.Type)
				.IsRequired() 
				.HasMaxLength(100); 

			builder.Property(r => r.Url)
				.IsRequired() 
				.HasMaxLength(500);

			builder.Property(r => r.UploadDate)
				.IsRequired()
				.HasDefaultValueSql("GETDATE()");

			
			builder.Property(r => r.AccessLevel)
				.IsRequired();
		}
	}
}
