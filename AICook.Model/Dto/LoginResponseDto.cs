namespace AICook.Model.Dto;

public record LoginResponseDto(
	string Token,
	UserDto User
);