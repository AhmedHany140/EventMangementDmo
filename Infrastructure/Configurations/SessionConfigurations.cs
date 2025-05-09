using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class SessionConfiguration : IEntityTypeConfiguration<Session>
	{
		public void Configure(EntityTypeBuilder<Session> builder)
		{
			// تعريف اسم الجدول
			builder.ToTable("Sessions");

			// تعريف المفتاح الأساسي
			builder.HasKey(s => s.Id);

			// تعريف الخصائص
			builder.Property(s => s.Title)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(s => s.Description)
				.HasMaxLength(1000); // اختياري

			builder.Property(s => s.StartTime)
				.IsRequired();

			builder.Property(s => s.EndTime)
				.IsRequired();

			builder.Property(s => s.MaxParticipants)
				.IsRequired();

			builder.Property(s => s.RecordingUrl)
				.HasMaxLength(500); // اختياري

			builder.Property(s => s.SessionLink)
				.HasMaxLength(500); // اختياري

			builder.Property(s => s.SessionStatus)
				.IsRequired()
				.HasConversion<string>(); // تحويل Enum إلى string

			builder.Property(s => s.SessionType)
				.IsRequired()
				.HasConversion<string>(); // تحويل Enum إلى string

			builder.HasOne(s => s.VirtualRoom)
		      .WithOne(vr => vr.Session)
		      .HasForeignKey<Session>(s => s.VirtualRoomId)
		     .OnDelete(DeleteBehavior.SetNull);

			// تعريف العلاقات مع الكيانات الأخرى
			builder.HasOne(s => s.Event)
				.WithMany(e => e.Sessions)
				.HasForeignKey(s => s.EventId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(s => s.Resources)
				.WithOne(r => r.Session)
				.HasForeignKey(r => r.SessionId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(s => s.Polls)
				.WithOne(p => p.Session)
				.HasForeignKey(p => p.SessionId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(s => s.ChatMessages)
				.WithOne(cm => cm.Session)
				.HasForeignKey(cm => cm.SessionId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(s => s.SessionSpeakers)
				.WithOne(ss => ss.Session)
				.HasForeignKey(ss => ss.SessionId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
