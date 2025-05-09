using Application.DTOs;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class PollMapper
{

	public partial Poll ToEntity(CreatePollDto dto);



	public partial PollDto ToDto(Poll entity);



	public partial void UpdateEntity(UpdatePollDto dto, Poll entity);

}