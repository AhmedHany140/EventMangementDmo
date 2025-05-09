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

			// إعداد المفتاح الأساسي
			builder.HasKey(r => r.Id);

			// إعداد العلاقات (Navigation properties)
			builder.HasOne(r => r.Session) // تحديد العلاقة مع Session
				.WithMany(s => s.Resources) // يمكن أن يكون لـ Session العديد من Resources
				.HasForeignKey(r => r.SessionId) // تعيين المفتاح الخارجي
				.OnDelete(DeleteBehavior.Cascade); // عند حذف الجلسة، يتم حذف الموارد المرتبطة بها

			builder.HasOne(r => r.Event) // تحديد العلاقة مع Event
				.WithMany(e => e.Resource) // يمكن أن يكون للـ Event العديد من Resources
				.HasForeignKey(r => r.EventId) // تعيين المفتاح الخارجي
				.OnDelete(DeleteBehavior.Cascade); // عند حذف الحدث، يتم حذف الموارد المرتبطة به

			// إعداد الخصائص الأخرى
			builder.Property(r => r.Name)
				.IsRequired() // اسم المورد يجب أن يكون مطلوبًا
				.HasMaxLength(255); // تعيين الحد الأقصى للطول

			builder.Property(r => r.Type)
				.IsRequired() // نوع المورد يجب أن يكون مطلوبًا
				.HasMaxLength(100); // تعيين الحد الأقصى للطول

			builder.Property(r => r.Url)
				.IsRequired() // URL يجب أن يكون مطلوبًا
				.HasMaxLength(500); // تعيين الحد الأقصى للطول

			builder.Property(r => r.UploadDate)
				.IsRequired()
				.HasDefaultValueSql("GETDATE()");

			// إعداد AccessLevel
			builder.Property(r => r.AccessLevel)
				.IsRequired(); // تحديد مستوى الوصول يجب أن يكون إلزاميًا
		}
	}
}
