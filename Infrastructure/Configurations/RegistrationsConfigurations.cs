using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class RegistrationsConfiguration : IEntityTypeConfiguration<Registrations>
	{
		public void Configure(EntityTypeBuilder<Registrations> builder)
		{
			builder.ToTable("Registrations");

			// إعداد المفتاح الأساسي
			builder.HasKey(r => r.Id);

			// إعداد علاقات (Navigation properties)
			builder.HasOne(r => r.Attendance) // تحديد العلاقة مع AppUser
				.WithMany(x=>x.Registrations) // يمكن أن يكون للـ AppUser العديد من التسجيلات
				.HasForeignKey(r => r.AttendanceId) // تعيين المفتاح الخارجي
				.OnDelete(DeleteBehavior.Cascade); // عند حذف المستخدم، يتم حذف التسجيلات المرتبطة

			builder.HasOne(r => r.Event) // تحديد العلاقة مع Event
				.WithMany(x=>x.Registrations) // يمكن أن يكون للـ Event العديد من التسجيلات
				.HasForeignKey(r => r.EventId) // تعيين المفتاح الخارجي
				.OnDelete(DeleteBehavior.Cascade); // عند حذف الحدث، يتم حذف التسجيلات المرتبطة

			builder.HasOne(r => r.TicketType) // تحديد العلاقة مع TicketType
				.WithMany(x=>x.Registrations) // يمكن أن يكون للـ TicketType العديد من التسجيلات
				.HasForeignKey(r => r.TicketTypeId) // تعيين المفتاح الخارجي
				.OnDelete(DeleteBehavior.Restrict); // لا يمكن حذف TicketType إذا كانت هناك تسجيلات مرتبطة

			// إعداد الخصائص الأخرى
			builder.Property(r => r.RegistrationDate)
				.IsRequired(); // تسجيل التاريخ يجب أن يكون مطلوبًا

			builder.Property(r => r.CheckInStatus)
				.IsRequired(); // حالة التسجيل يجب أن تكون مطلوبة

			builder.Property(r => r.ChickInTime);
			
		}
	}
}
