using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class SponsorConfiguration : IEntityTypeConfiguration<Sponsor>
	{
		public void Configure(EntityTypeBuilder<Sponsor> builder)
		{
			builder.ToTable("Sponsors");

			builder.HasKey(s => s.Id);

			builder.Property(s => s.Name)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(s => s.LogoUrl)
				.HasMaxLength(500); // Optional

			builder.Property(s => s.Website)
				.HasMaxLength(500); // Optional

			builder.Property(s => s.ContactPerson)
				.HasMaxLength(200); // Optional

			builder.Property(s => s.Email)
				.HasMaxLength(200); // Optional

			builder.Property(s => s.SponsorLevel)
				.IsRequired()
				.HasConversion<string>(); // Store as string (e.g., "Bronze", "Silver", "Platinum")

			// Relationships
			builder.HasMany(s => s.EventSponsors)
				.WithOne(es => es.Sponsor)
				.HasForeignKey(es => es.SponsorId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
