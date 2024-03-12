namespace AICook.Model.Dto;

public record UserUpdateDto(
	string? Email,
	string? FullName,
	UserRole? Role
);