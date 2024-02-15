using AICook.Event.Contracts;
using AICook.Event.Contracts.Jwt;
using AICook.Identity.Services;
using AICook.Model.Dto;
using AutoMapper;
using MassTransit;

namespace AICook.Identity.Consumers;

public class JwtVerificationRequestConsumer(
	IJwtService jwtService,
	IUserService userService,
	ILogger<JwtVerificationRequestConsumer> logger
) : IConsumer<JwtVerificationRequest>
{
	public async Task Consume(ConsumeContext<JwtVerificationRequest> context)
	{
		var userId = await jwtService.ValidateJwtToken(context.Message.Token);
		if (userId == null)
		{
			await Failure(context);
			return;
		}
		
		var user = await userService.GetById(userId.Value);
		if (user == null)
		{
			await Failure(context);
			return;
		}

		await context.RespondAsync(
			new JwtVerificationRequestSuccess(
				user.Id,
				user.Role
			)
		);
	}

	private static async Task Failure(ConsumeContext context)
	{
		await context.RespondAsync(new JwtVerificationRequestFailure());
	}
}