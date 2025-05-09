using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class PollConfiguration : IEntityTypeConfiguration<Poll>
	{
		public void Configure(EntityTypeBuilder<Poll> builder)
		{
			builder.ToTable("Polls");

			// تحديد المفتاح الأساسي
			builder.HasKey(p => p.Id);

			// تحديد الخصائص
			builder.Property(p => p.Question)
				.IsRequired() // يجب أن يكون السؤال إلزاميًا
				.HasMaxLength(500); // تحديد الطول الأقصى للسؤال

			builder.Property(p => p.IsAnonymous)
				.IsRequired(); // تحديد أن "هل الاستطلاع مجهول" إلزامي

			builder.Property(p => p.CreatedAt)
				.IsRequired()
				.HasDefaultValueSql("GETDATE()"); // تحديد التاريخ الافتراضي ليكون تاريخ اليوم

			builder.Property(p => p.PollType)
				.IsRequired() // يجب أن يكون نوع الاستطلاع إلزاميًا
				.HasConversion<string>(); // تخزين النوع كـ string (مثل "MultipleChoice", "TrueFalse")

			// تحديد العلاقة مع الكيان Session
			builder.HasOne(p => p.Session)
				.WithMany(s => s.Polls)
				.HasForeignKey(p => p.SessionId)
				.OnDelete(DeleteBehavior.Cascade); // عند حذف الجلسة، يتم حذف الاستطلاع أيضًا




			
		}
	}
}
