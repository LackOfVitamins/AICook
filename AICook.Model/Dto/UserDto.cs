namespace AICook.Model.Dto;

public record UserDto(
	Guid Id,
	string Email,
	string FullName,
	UserRole Role
);