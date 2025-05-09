using Domain.Response;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public abstract class GlobalController<Tcreate, TDto, TUpdateDto> : ControllerBase
{
	[HttpGet("GetAll")]
	public abstract Task<ActionResult<IEnumerable<TDto>>> GetAll();

	[HttpGet("{id:int}")]
	public abstract Task<ActionResult<TDto>> GetById( int id);

	[HttpPost]
	public abstract Task<ActionResult<Response>> Create(Tcreate dto);

	[HttpPut]
	public abstract Task<ActionResult<Response>> Update(TUpdateDto dto);

	[HttpDelete("{id}")]
	public abstract Task<ActionResult<Response>> Delete(int id);
}
