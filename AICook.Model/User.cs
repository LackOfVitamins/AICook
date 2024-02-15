using System.ComponentModel.DataAnnotations;

namespace AICook.Model;

public class User
{
	public Guid Id { get; set; }
	
	[EmailAddress]
	public string Email { get; set; }
	
	public UserRole Role { get; set; }
	public string? PasswordHash { get; set; }
}


