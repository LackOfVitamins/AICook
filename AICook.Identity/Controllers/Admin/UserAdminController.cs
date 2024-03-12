using System.ComponentModel.DataAnnotations;
using AICook.Identity.Exceptions.User;
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
			return Problem(
				statusCode: StatusCodes.Status404NotFound,
				detail: "User does not exist!"
			);
		
		return Ok(
			mapper.Map<UserDto>(user)
		);
	}
	
	[HttpPut("{id:guid}")]
	public async Task<ActionResult<UserDto>> Update(Guid id, UserUpdateDto dto)
	{
		// if (!string.IsNullOrEmpty(dto.Id.ToString()) && id != dto.Id)
		// 	return Problem(
		// 		statusCode: StatusCodes.Status400BadRequest,
		// 		detail: "Ids are not equal."
		// 	);

		try
		{
			var user = await userService.Update(id, dto);
			
			return Ok(
				mapper.Map<UserDto>(user)
			);
		}
		catch (UserNotFoundException e)
		{
			return Problem(
				statusCode: StatusCodes.Status404NotFound,
				detail: e.Message
			);
		}
	}
	
	[HttpPost]
	public async Task<ActionResult<UserDto>> Create([FromBody] RegisterDto dto)
	{
		try
		{
			var user = await userService.Register(dto);

			return Created(
				Url.Action("Get", new { id = user.Id }),
				mapper.Map<UserDto>(user)
			);
		}
		catch (ValidationException e)
		{
			return Problem(
				statusCode: StatusCodes.Status422UnprocessableEntity,
				detail: e.Message
			);
		}
	}
}