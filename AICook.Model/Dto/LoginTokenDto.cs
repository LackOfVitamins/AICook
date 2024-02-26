namespace AICook.Model.Dto;

public record LoginTokenDto(
	Guid Id,
	UserDto User,
	int UseCount,
	DateTime? LastUsed,
	DateTime Expires
);