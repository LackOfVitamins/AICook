using System.ComponentModel.DataAnnotations;

namespace AICook.Model.Dto;

public record RegisterDto(
	[Required]
	string Email,
	[Required]
	string Password,
	[Required]
	string FullName,
	[Required]
	UserRole Role
);
