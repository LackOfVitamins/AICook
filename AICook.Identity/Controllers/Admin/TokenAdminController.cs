using AICook.Identity.Exceptions.User;
using AICook.Identity.Services;
using AICook.Model;
using AICook.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RandomString4Net;

namespace AICook.Identity.Controllers.Admin;

[Authorize(Roles = nameof(UserRole.Admin))]
[Route("identity/admin/token")]
[ApiController]
public class TokenAdminController(
	IUserService userService,
	ITokenService tokenService,
	IMapper mapper
) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<LoginTokenDto>>> Get()
	{
		var loginTokens = await tokenService.Get();
		return Ok(
			loginTokens.Select(mapper.Map<LoginTokenDto>)
		);
	}

	[HttpPost]
	public async Task<ActionResult<LoginTokenCreateResponseDto>> Create([FromBody] LoginTokenCreateDto model)
	{
		var user = await userService.Get(model.UserId);
		
		if(user == null)
			return Problem(
				statusCode: StatusCodes.Status422UnprocessableEntity,
				detail: "User does not exist!"
			);
		
		var randomString = GenerateRandomString();
		await tokenService.Create(user, randomString);

		return Ok(
			new LoginTokenCreateResponseDto(
				randomString
			)
		);
	}

	private static string GenerateRandomString()
	{
		return RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 40);
	}
}