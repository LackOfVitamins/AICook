using AICook.Identity.Data;
using AICook.Model;
using AICook.Model.Dto;
using Microsoft.EntityFrameworkCore;

namespace AICook.Identity.Services;

public interface IUserService
{
	Task<User?> Authenticate(LoginDto model);
	Task<User?> Register(RegisterDto model);
	Task<User?> GetByEmail(string email);
	Task<User?> GetById(Guid id);
}

public class UserService(
	IdentityContext context
) : IUserService
{
	private const int BCryptWorkFactor = 13;
	
	public async Task<User?> Authenticate(LoginDto model)
	{
		var user = await GetByEmail(model.Email);

		if(user == null)
			return null;
		
		if(user.PasswordHash == null)
			return null;
		
		if(!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
			return null;

		return user;
	}

	public async Task<User?> Register(RegisterDto model)
	{
		var passwordHashed = BCrypt.Net.BCrypt.HashPassword(model.Password, BCryptWorkFactor);
		
		var entry = await context.Users.AddAsync(
			new User
			{
				Email = model.Email,
				PasswordHash = passwordHashed,
				Role = model.Role
			}
		);

		return entry.Entity;
	}

	public async Task<User?> GetByEmail(string email)
	{
		return await context.Users.SingleOrDefaultAsync(x => x.Email == email);
	}

	public async Task<User?> GetById(Guid id)
	{
		return await context.Users.FindAsync(id);
	}
}