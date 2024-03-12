using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using AICook.Identity.Exceptions.User;
using AICook.Identity.Services;
using AICook.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AICook.Identity.Controllers;

[Route("[controller]")]
[ApiController]
public class IdentityController(
	IJwtService jwtService,
	IUserService userService,
	IMapper mapper
)
: ControllerBase
{
	[HttpPost("login")]
	public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto model)
	{
		try
		{
			var user = await userService.Authenticate(model);

			var token = jwtService.CreateJwtToken(user);
			return Ok(
				new LoginResponseDto(
					jwtService.WriteJwtToken(token),
					mapper.Map<UserDto>(user)
				)
			);
		}
		catch (AuthenticationException e)
		{
			return Problem(
				statusCode: StatusCodes.Status400BadRequest, 
				detail: e.Message
			);
		}
	}
	
	[HttpPost("token")]
	public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginTokenLoginDto model)
	{
		try
		{
			var user = await userService.Authenticate(model);

			var token = jwtService.CreateJwtToken(user);
			return Ok(
				new LoginResponseDto(
					jwtService.WriteJwtToken(token),
					mapper.Map<UserDto>(user)
				)
			);
		}
		catch (AuthenticationException e)
		{
			return Problem(
				statusCode: StatusCodes.Status400BadRequest,
				detail: e.Message
			);
		}
		catch (UserBlockedException e)
		{
			return Problem(
				statusCode: StatusCodes.Status403Forbidden,
				detail: e.Message
			);
		}
	}

	[Authorize]
	[HttpGet("user")]
	public async Task<ActionResult<UserDto>> CurrentUserInfo()
	{
		try
		{
			var user = await userService.Get(HttpContext.User);

			return Ok(
				mapper.Map<UserDto>(user)
			);
		}
		catch (ValidationException e)
		{
			return Problem(
				statusCode: StatusCodes.Status400BadRequest,
				detail: e.Message
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
}