using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class VirtualRoomConfiguration : IEntityTypeConfiguration<VirtualRoom>
	{
		public void Configure(EntityTypeBuilder<VirtualRoom> builder)
		{
			builder.ToTable("VirtualRooms");

			// إعداد المفتاح الأساسي
			builder.HasKey(vr => vr.Id); // RoomId سيكون هو المفتاح الأساسي

			// إعداد العلاقات
			builder.HasOne(vr => vr.Session) // العلاقة مع الكائن Session
				.WithOne(s => s.VirtualRoom) // لكل Session يوجد VirtualRoom واحد فقط
				.HasForeignKey<VirtualRoom>(vr => vr.SessionId) // SessionId هو المفتاح الخارجي
				.OnDelete(DeleteBehavior.Cascade); // حذف VirtualRoom عندما يتم حذف Session

			// إعداد الخصائص الأخرى
			builder.Property(vr => vr.Name)
				.IsRequired() // يجب أن يكون الحقل مطلوبًا
				.HasMaxLength(200); // تعيين الحد الأقصى للطول

			builder.Property(vr => vr.Platform)
				.IsRequired() // يجب أن يكون الحقل مطلوبًا
				.HasMaxLength(100); // تعيين الحد الأقصى للطول

			builder.Property(vr => vr.AccessCode)
				.IsRequired() // يجب أن يكون الحقل مطلوبًا
				.HasMaxLength(50); // تعيين الحد الأقصى للطول
		}
	}
}
