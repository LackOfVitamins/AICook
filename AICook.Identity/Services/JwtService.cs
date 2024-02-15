using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AICook.Model;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AICook.Identity.Services;

public record JwtServiceConfiguration(
	[Required]
	string Secret
);

public interface IJwtService
{
	public SecurityToken CreateJwtToken(User user);
	public string WriteJwtToken(SecurityToken token);
	public Task<Guid?> ValidateJwtToken(string token);
}

public class JwtService : IJwtService
{
	private JwtServiceConfiguration Configuration { get; }
	
	public JwtService(IConfiguration configuration)
	{
		var jwtConfig = configuration.GetSection("JWT").Get<JwtServiceConfiguration>();
		Configuration = jwtConfig ?? throw new InvalidConfigurationException("No JWT section in config found!");
	}
	
	public SecurityToken CreateJwtToken(User user)
	{ 
		var key = Encoding.ASCII.GetBytes(Configuration.Secret);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(
				new[]
				{
					new Claim("id", user.Id.ToString()),
				}
			),
			Expires = DateTime.UtcNow.AddHours(4),
			SigningCredentials = new SigningCredentials(
				new SymmetricSecurityKey(key),
				SecurityAlgorithms.HmacSha256Signature
			)
		};
		
		return new JwtSecurityTokenHandler()
			.CreateToken(tokenDescriptor);
	}

	public string WriteJwtToken(SecurityToken token)
	{
		return new JwtSecurityTokenHandler()
			.WriteToken(token);
	}

	public async Task<Guid?> ValidateJwtToken(string token)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(Configuration.Secret);
		try
		{
			var result = await tokenHandler.ValidateTokenAsync(
				token,
				new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}
			);
			var jwtToken = (JwtSecurityToken) result.SecurityToken;
			var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

			// return user id from JWT token if validation successful
			return userId;
		}
		catch
		{
			// return null if validation fails
			return null;
		}
	} 
}