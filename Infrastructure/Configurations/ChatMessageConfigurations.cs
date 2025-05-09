using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
	{
		public void Configure(EntityTypeBuilder<ChatMessage> builder)
		{
			builder.ToTable("ChatMessages");

			// Primary key
			builder.HasKey(cm => cm.Id);

			// Session relationship
			builder.HasOne(cm => cm.Session)
				.WithMany(s => s.ChatMessages)
				.HasForeignKey(cm => cm.SessionId)
				.OnDelete(DeleteBehavior.NoAction); // تم التعديل من Cascade إلى Restrict

			// User relationship
			builder.HasOne(cm => cm.User)
				.WithMany(u => u.ChatMessages)
				.HasForeignKey(cm => cm.UserId)
				.OnDelete(DeleteBehavior.NoAction); // تم التعديل من Cascade إلى Restrict

			// ParentMessage relationship (self-reference)
			builder.HasOne(cm => cm.ParentMessage)
				.WithMany(pm => pm.Replies)
				.HasForeignKey(cm => cm.ParentMessageId)
				.OnDelete(DeleteBehavior.NoAction); // إبقاءه Restrict مهم لتجنب الحذف الحلقي

			builder.Property(p => p.Timestamp)
				.IsRequired()
				.HasDefaultValueSql("GETDATE()");

			builder.Property(cm => cm.Text)
				.IsRequired()
				.HasMaxLength(1000);
		}
	}
}
