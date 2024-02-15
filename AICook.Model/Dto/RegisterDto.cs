namespace AICook.Model.Dto;

public record RegisterDto(
	string Email,
	string Password,
	UserRole Role
);
