namespace AICook.Model.Dto;

public record UserDto(
	string Id,
	string Email,
	UserRole Role
);