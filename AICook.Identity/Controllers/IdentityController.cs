using System.Security.Claims;
using AICook.Identity.Services;
using AICook.Model;
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
		var user = await userService.Authenticate(model);

		if (user == null)
			return Unauthorized();

		var token = jwtService.CreateJwtToken(user);
		return Ok(
			new LoginResponseDto(
				jwtService.WriteJwtToken(token),
				mapper.Map<UserDto>(user)
			)
		);
	}
	
	[HttpPost("token")]
	public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginTokenDto model)
	{
		// if(Guid.TryParse(model.))
		var user = await userService.Authenticate(model);

		if (user == null)
			return Unauthorized();

		var token = jwtService.CreateJwtToken(user);
		return Ok(
			new LoginResponseDto(
				jwtService.WriteJwtToken(token),
				mapper.Map<UserDto>(user)
			)
		);
	}

	[Authorize]
	[HttpGet("user")]
	public async Task<ActionResult<UserDto>> UserInfo()
	{
		var user = await userService.Get(HttpContext.User);
		
		if(user == null)
			return BadRequest();

		return Ok(
			mapper.Map<UserDto>(user)
		);
	}
}