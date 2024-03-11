namespace AICook.Model.Dto;

public record RegisterDto(
	string Email,
	string Password,
	string FullName,
	UserRole Role
);
