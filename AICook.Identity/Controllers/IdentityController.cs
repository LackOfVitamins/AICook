using AICook.Identity.Services;
using AICook.Model;
using AICook.Model.Dto;
using AutoMapper;
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
}