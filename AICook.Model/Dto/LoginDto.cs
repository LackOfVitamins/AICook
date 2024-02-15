using System.ComponentModel.DataAnnotations;

namespace AICook.Model.Dto;

public record LoginDto(
	[Required]
	string Email,
	[Required]
	string Password
);

