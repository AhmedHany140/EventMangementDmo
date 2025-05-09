using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class EventSponsorConfiguration : IEntityTypeConfiguration<EventSponsor>
	{
		public void Configure(EntityTypeBuilder<EventSponsor> builder)
		{
			builder.ToTable("EventSponsors");

		
			builder.HasKey(es => new { es.EventId, es.SponsorId });


			builder.HasOne(es => es.Event)
				   .WithMany(e => e.EventSponsors)  
				   .HasForeignKey(es => es.EventId)
				   .IsRequired()
				   .OnDelete(DeleteBehavior.Cascade); 

		
			builder.HasOne(es => es.Sponsor)
				   .WithMany(s => s.EventSponsors) 
				   .HasForeignKey(es => es.SponsorId)
				   .IsRequired()
				   .OnDelete(DeleteBehavior.Cascade); 
		
			builder.Property(es => es.Amount)
				   .IsRequired();  

			builder.Property(es => es.SponsorshipLevel)
				   .IsRequired()  
				   .HasConversion<string>();  
		}
	}
}
