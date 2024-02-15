using AICook.Identity.Services;
using AICook.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AICook.Identity.Controllers;

[Route("[controller]")]
[ApiController]
public class IdentityController(
	IJwtService jwtService,
	IUserService userService
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
				jwtService.WriteJwtToken(token)
				// token.ValidTo
			)
		);
	}
}