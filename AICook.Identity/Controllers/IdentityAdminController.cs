using System.Drawing;
using AICook.Identity.Services;
using AICook.Model;
using AICook.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RandomString4Net;

namespace AICook.Identity.Controllers;

[Authorize(Roles = nameof(UserRole.Admin))]
[Route("identity/admin")]
[ApiController]
public class IdentityAdminController(
	IUserService userService,
	ITokenService tokenService
) : ControllerBase
{
	[HttpPost("token/create")]
	public async Task<ActionResult<TokenCreateResponseDto>> Create([FromBody] TokenCreateDto model)
	{
		var user = await userService.Get(model.UserId);

		if (user == null)
			return UnprocessableEntity("User is not found!");
		
		var randomString = GenerateRandomString();
		var loginToken = await tokenService.Create(user, randomString);
		if (loginToken == null)
			return StatusCode(500);
		
		return Ok(
			new TokenCreateResponseDto(
				randomString
			)
		);
	}

	private static string GenerateRandomString()
	{
		return RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 40);
	}
}