

using Application.DTOs;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper
{
	[Mapper]
	public partial class ChatMessageMapper
	{
		public partial ChatMessage ToEntity(CreateChatMessageDto dto);

		[MapProperty(nameof(ChatMessage.User.UserName), nameof(ChatMessageDto.UserName))]
		public partial ChatMessageDto ToDto(ChatMessage Entity);

		public partial void UpdateEntity(UpdateChatMessageDto dto, ChatMessage entity);


	}


}
