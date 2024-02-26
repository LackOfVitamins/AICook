using AICook.Identity.Services;
using AICook.Model;
using AICook.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AICook.Identity.Controllers.Admin;

[Authorize(Roles = nameof(UserRole.Admin))]
[Route("identity/admin/user")]
[ApiController]
public class UserAdminController(
    IUserService userService,
    IMapper mapper
) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserDto>>> Get()
	{
		var users = await userService.Get();
		return Ok( 
			users.Select(mapper.Map<UserDto>)
		);
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<UserDto>> Get(Guid id)
	{
		var user = await userService.Get(id);
		
		if (user == null)
			return NotFound();

		return Ok(
			mapper.Map<UserDto>(user)
		);
	}
	
	[HttpPost]
	public async Task<ActionResult<UserDto>> Create([FromBody] RegisterDto dto)
	{
		var user = await userService.Register(dto);

		if (user == null)
			return UnprocessableEntity();
		
		return Created(
			Url.Action("Get", new { id = user.Id }),
			mapper.Map<UserDto>(user)
		);
	}
}