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

			// إعداد المفتاح الأساسي
			builder.HasKey(ss => new { ss.SessionId, ss.SpeakerId }); // العلاقة بين Session و Speaker هي علاقة متعددة الجوانب (Many-to-Many)

			// إعداد العلاقات
			builder.HasOne(ss => ss.Session) // تحديد العلاقة مع Session
				.WithMany(s => s.SessionSpeakers) // يمكن أن يكون لكل Session العديد من المتحدثين
				.HasForeignKey(ss => ss.SessionId) // تعيين المفتاح الخارجي
				.OnDelete(DeleteBehavior.Cascade); // عند حذف الجلسة، يتم حذف المتحدثين المرتبطين بها

			builder.HasOne(ss => ss.Speaker) // تحديد العلاقة مع AppUser (المتحدث)
				.WithMany(u => u.SessionSpeakers) // يمكن أن يكون لكل متحدث العديد من الجلسات
				.HasForeignKey(ss => ss.SpeakerId) // تعيين المفتاح الخارجي
				.OnDelete(DeleteBehavior.Cascade); // عند حذف المتحدث، يتم حذف الارتباطات المرتبطة به

			// إعداد الخصائص الأخرى
			builder.Property(ss => ss.Role)
				.IsRequired() // يجب أن يكون الحقل مطلوبًا
				.HasMaxLength(100); // تعيين الحد الأقصى للطول
		}
	}
}
