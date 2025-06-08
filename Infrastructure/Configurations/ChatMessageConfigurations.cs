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

		
			builder.HasKey(cm => cm.Id);

			
			builder.HasOne(cm => cm.Session)
				.WithMany(s => s.ChatMessages)
				.HasForeignKey(cm => cm.SessionId)
				.OnDelete(DeleteBehavior.NoAction); 


			builder.HasOne(cm => cm.User)
				.WithMany(u => u.ChatMessages)
				.HasForeignKey(cm => cm.UserId)
				.OnDelete(DeleteBehavior.NoAction); 

	
			builder.HasOne(cm => cm.ParentMessage)
				.WithMany(pm => pm.Replies)
				.HasForeignKey(cm => cm.ParentMessageId)
				.OnDelete(DeleteBehavior.NoAction); 

			builder.Property(p => p.Timestamp)
				.IsRequired()
				.HasDefaultValueSql("GETDATE()");

			builder.Property(cm => cm.Text)
				.IsRequired()
				.HasMaxLength(1000);
		}
	}
}
