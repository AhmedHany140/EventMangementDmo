using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class TicketTypeConfigurations : IEntityTypeConfiguration<TicketType>
	{
		public void Configure(EntityTypeBuilder<TicketType> builder)
		{
			builder.ToTable("Tickets");

			builder.HasKey(x => x.Id);

			builder.Property(t => t.QuantityAvailable)
	        .IsRequired();

			builder.Property(t => t.Price)
				   .IsRequired();

			builder.Property(t => t.Name)
				   .IsRequired()
				   .HasMaxLength(200);

			builder.Property(t => t.SalesStartDate)
				   .IsRequired();

			builder.Property(t => t.SalesEndDate)
				   .IsRequired();


			builder.HasOne(x=>x.Event)
				.WithMany(x=>x.TicketTypes)
				.HasForeignKey(x=>x.EventId)
				.IsRequired()
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(x => x.Registrations)
				.WithOne(x => x.TicketType)
				.HasForeignKey(x => x.TicketTypeId)
				.OnDelete(DeleteBehavior.Restrict);

		}
	}
}
