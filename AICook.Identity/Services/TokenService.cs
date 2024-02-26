using AICook.Identity.Data;
using AICook.Model;
using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AICook.Identity.Services;

public interface ITokenService
{
	public Task<IEnumerable<LoginToken>> Get();
	public Task<LoginToken?> Get(Guid tokenId);
	public Task<LoginToken?> Create(User user, string token);
	public Task Update(LoginToken loginToken);
}

public class TokenService(IdentityContext context) : ITokenService
{
	private const int TokenExpiresAfterDays = 30;

	public async Task<IEnumerable<LoginToken>> Get()
	{
		return await context.Tokens
			.Include(x => x.User)
			.ToListAsync();
	}

	public async Task<LoginToken?> Get(Guid tokenId)
	{
		return await context.Tokens
			.Include(x => x.User)
			.SingleOrDefaultAsync(x => x.Id == tokenId);
	}

	public async Task<LoginToken?> Create(User user, string token)
	{
		var hash = Argon2.Hash(token);
		
		var loginToken = new LoginToken
		{
			TokenHash = hash,
			Expires = DateTime.Now.AddDays(TokenExpiresAfterDays),
			User = user
		};
		
		var entry = await context.Tokens.AddAsync(loginToken);
		await context.SaveChangesAsync();

		return entry.Entity;
	}

	public async Task Update(LoginToken loginToken)
	{
		context.Tokens.Update(loginToken);
		await context.SaveChangesAsync();
	}
}