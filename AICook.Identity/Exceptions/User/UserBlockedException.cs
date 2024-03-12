namespace AICook.Identity.Exceptions.User;

public class UserBlockedException : Exception
{
	public UserBlockedException()
		: base() 
	{}

	public UserBlockedException(string message)
		: base(message)
	{}

	public UserBlockedException(string message, Exception inner)
		: base(message, inner)
	{}
}