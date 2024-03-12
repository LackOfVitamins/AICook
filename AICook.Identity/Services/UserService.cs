using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Security.Claims;
using AICook.Identity.Data;
using AICook.Identity.Exceptions.User;
using AICook.Model;
using AICook.Model.Dto;
using AutoMapper;
using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;

namespace AICook.Identity.Services;

public interface IUserService
{
	Task<User> Authenticate(LoginDto model);
	Task<User> Authenticate(LoginTokenLoginDto model);
	Task<User> Register(RegisterDto model);
	Task<User> Update(Guid id, UserUpdateDto model);
	Task<IEnumerable<User>> Get();
	Task<User?> Get(string email);
	Task<User?> Get(Guid id);
	Task<User> Get(ClaimsPrincipal claimsPrincipal);
}

public class UserService(
	IdentityContext context,
	ITokenService tokenService,
	IMapper mapper
) : IUserService
{
	public async Task<User> Authenticate(LoginDto model)
	{
		var user = await Get(model.Email);
		if (user == null)
			throw new AuthenticationException("Incorrect email or password.");
		
		if (user.Blocked)
			throw new UserBlockedException("Your account is blocked!");

		if (user.PasswordHash == null)
			throw new AuthenticationException("You can not login using a password.");

		if (!Argon2.Verify(user.PasswordHash, model.Password))
			throw new AuthenticationException("Incorrect email or password.");

		return user;
	}

	public async Task<User> Authenticate(LoginTokenLoginDto model)
	{
		var loginToken = await tokenService.Get(model.Id);

		if (loginToken == null)
			throw new AuthenticationException("Incorrect token!");

		if (loginToken.Expires <= DateTime.Now)
			throw new AuthenticationException("Token is expired!");
		
		if (!Argon2.Verify(loginToken.TokenHash, model.Token))
			throw new AuthenticationException("Incorrect token!");
		
		loginToken.UseCount++;
		loginToken.LastUsed = DateTime.Now;
		await tokenService.Update(loginToken);

		var user = loginToken.User;
		if (user.Blocked)
			throw new UserBlockedException("User is blocked!");
		
		return user;
	}

	public async Task<User> Register(RegisterDto model)
	{
		var passwordHashed = Argon2.Hash(model.Password);
		
		var entry = await context.Users.AddAsync(
			new User
			{
				Email = model.Email,
				FullName = model.FullName,
				PasswordHash = passwordHashed,
				Role = model.Role
			}
		);
 
		await context.SaveChangesAsync();
		return entry.Entity;
	}
	
	public async Task<User> Update(Guid id, UserUpdateDto model)
	{
		var user = await Get(id);

		if (user == null)
			throw new UserNotFoundException("User does not exist!");
		
		mapper.Map(model, user);
		await context.SaveChangesAsync();
		
		return user;
	}

	public async Task<IEnumerable<User>> Get()
	{
		return await context.Users.ToListAsync();
	}

	public async Task<User?> Get(string email)
	{
		var user = await context.Users.SingleOrDefaultAsync(x => x.Email == email);
		
		// if (user == null)
		// 	throw new UserNotFoundException("User is not found!");
		
		return user;
	}

	public async Task<User?> Get(Guid id)
	{ 
		var user = await context.Users.FindAsync(id);

		// if (user == null)
		// 	throw new UserNotFoundException("User is not found!");
		
		return user;
	}

	public async Task<User> Get(ClaimsPrincipal claimsPrincipal)
	{
		var nameId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (nameId == null)
			throw new ValidationException("Token is not valid!");

		if (!Guid.TryParse(nameId, out var guid))
			throw new ValidationException("Token is not valid!");

		var user = await Get(guid);
		if(user == null)
			throw new UserNotFoundException("User is not found!");
		
		return user;
	}
}