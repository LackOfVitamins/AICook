namespace AICook.Model.Dto;

public record UserDto(
	string Id,
	string Email,
	string FullName,
	UserRole Role
);