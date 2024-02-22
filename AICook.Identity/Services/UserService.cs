using System.Security.Claims;
using AICook.Identity.Data;
using AICook.Model;
using AICook.Model.Dto;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AICook.Identity.Services;

public interface IUserService
{
	Task<User?> Authenticate(LoginDto model);
	Task<User?> Authenticate(LoginTokenDto model);
	Task<User?> Register(RegisterDto model);
	Task<User?> Get(string email);
	Task<User?> Get(Guid id);
	Task<User?> Get(ClaimsPrincipal claimsPrincipal);
}

public class UserService(
	IdentityContext context,
	ITokenService tokenService
) : IUserService
{
	public async Task<User?> Authenticate(LoginDto model)
	{
		var user = await Get(model.Email);

		if(user == null)
			return null;
		
		if(user.PasswordHash == null)
			return null;
		
		if (!Argon2.Verify(user.PasswordHash, model.Password))
			return null;

		return user;
	}

	public async Task<User?> Authenticate(LoginTokenDto model)
	{
		var loginToken = await tokenService.Get(model.Id);

		if (loginToken == null)
			return null;

		if (loginToken.Expires <= DateTime.Now)
			return null;
		
		if (!Argon2.Verify(loginToken.TokenHash, model.Token))
			return null;
		
		loginToken.UseCount++;
		loginToken.LastUsed = DateTime.Now;
		await tokenService.Update(loginToken);

		return loginToken.User;
	}

	public async Task<User?> Register(RegisterDto model)
	{
		var passwordHashed = Argon2.Hash(model.Password);
		
		var entry = await context.Users.AddAsync(
			new User
			{
				Email = model.Email,
				PasswordHash = passwordHashed,
				Role = model.Role
			}
		);

		await context.SaveChangesAsync();
		return entry.Entity;
	}

	public async Task<User?> Get(string email)
	{
		return await context.Users.SingleOrDefaultAsync(x => x.Email == email);
	}

	public async Task<User?> Get(Guid id)
	{
		return await context.Users.FindAsync(id);
	}

	public async Task<User?> Get(ClaimsPrincipal claimsPrincipal)
	{
		var nameId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		
		if(nameId == null)
			return null;

		if (!Guid.TryParse(nameId, out var guid))
			return null;

		return await Get(guid);
	}
}