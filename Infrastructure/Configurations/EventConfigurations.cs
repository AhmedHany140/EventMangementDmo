using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class EventConfiguration : IEntityTypeConfiguration<Event>
	{
		public void Configure(EntityTypeBuilder<Event> builder)
		{
			// اسم الجدول
			builder.ToTable("Events");

			// المفتاح الأساسي
			builder.HasKey(e => e.Id);

			// الخصائص المطلوبة أو الاختيارية
			builder.Property(e => e.Title)
				   .IsRequired()
				   .HasMaxLength(200);

			builder.Property(e => e.Description)
				   .HasMaxLength(1000); // Optional

			builder.Property(e => e.StartDate)
				   .IsRequired();

			builder.Property(e => e.EndDate)
				   .IsRequired();

			builder.Property(e => e.EvenentStatus)
				   .IsRequired()
				   .HasConversion<string>();

			builder.Property(e => e.EventType)
				   .IsRequired()
				   .HasConversion<string>();

			builder.Property(e => e.MaxAttendees); // Optional

			builder.Property(e => e.IsRecurring)
				   .IsRequired();

			builder.Property(e => e.RecurrencePattern)
				   .HasConversion<string>(); // Optional (used only if IsRecurring == true)

			builder.Property(e => e.CoverImageUrl)
				   .HasMaxLength(500); // Optional

			builder.Property(e => e.TimeZone)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(e => e.RegistrationDeadline); // Optional

			// العلاقة مع Organizer (AppUser)
			builder.HasOne(e => e.Organizer)
				   .WithMany(x => x.Events)
				   .HasForeignKey(e => e.OrganizerId)
				   .IsRequired()
				   .OnDelete(DeleteBehavior.Restrict);

			// العلاقات (Navigation Collections)
			builder.HasMany(e => e.Registrations)
				   .WithOne(r => r.Event)
				   .HasForeignKey(r => r.EventId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(e => e.TicketTypes)
				   .WithOne(t => t.Event)
				   .HasForeignKey(t => t.EventId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(e => e.Sessions)
				   .WithOne(s => s.Event)
				   .HasForeignKey(s => s.EventId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(e => e.EventSponsors)
				   .WithOne(es => es.Event)
				   .HasForeignKey(es => es.EventId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(e => e.Resource)
				   .WithOne(r => r.Event)
				   .HasForeignKey(r => r.EventId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
