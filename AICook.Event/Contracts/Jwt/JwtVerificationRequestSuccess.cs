using AICook.Model;

namespace AICook.Event.Contracts.Jwt;

public record JwtVerificationRequestSuccess(
	Guid UserId,
	UserRole Role
);