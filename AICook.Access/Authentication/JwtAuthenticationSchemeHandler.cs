using System.Security.Claims;
using System.Text.Encodings.Web;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AICook.Event.Contracts.Jwt;

namespace AICook.Authorization.Authentication;

public class JwtAuthenticationSchemeHandler(
	IRequestClient<JwtVerificationRequest> requestClient,
	IOptionsMonitor<JwtAuthenticationSchemeOptions> options,
	ILoggerFactory logger,
	UrlEncoder encoder
) : AuthenticationHandler<JwtAuthenticationSchemeOptions>(
	options,
	logger,
	encoder
)
{
	protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		if (Request.Headers.Authorization.Count <= 0)
			return AuthenticateResult.Fail("Unauthorized");

		var authorizationHeader = Request.Headers.Authorization.ToString();
		if (string.IsNullOrEmpty(authorizationHeader))
		{
			return AuthenticateResult.NoResult();
		}

		if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
		{
			return AuthenticateResult.Fail("Unauthorized");
		}

		var token = authorizationHeader.Substring("bearer".Length).Trim();
		if (string.IsNullOrEmpty(token))
		{
			return AuthenticateResult.Fail("Unauthorized");
		}

		var response = await requestClient.GetResponse<JwtVerificationRequestSuccess, JwtVerificationRequestFailure>(
			new JwtVerificationRequest(
				token
			)
		);

		if (response.Is(out Response<JwtVerificationRequestFailure>? _))
			return AuthenticateResult.Fail("Unauthorized");

		if (response.Is(out Response<JwtVerificationRequestSuccess>? responseSuccess))
		{
			var message = responseSuccess.Message;
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, message.UserId.ToString()),
				new Claim(ClaimTypes.Role, message.Role.ToString())
			};

			var claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
			var authTicket = new AuthenticationTicket(
				claimsPrincipal,
				Scheme.Name
			);

			return AuthenticateResult.Success(authTicket);
		}
		
		return AuthenticateResult.Fail("Unauthorized");
	}
}