namespace AICook.Model;

public class LoginToken
{
	public Guid Id { get; init; }
	public string TokenHash { get; init; }
	public int UseCount { get; set; }
	public DateTime? LastUsed { get; set; }
	public DateTime Expires { get; init; }
	public User User { get; init; }
}