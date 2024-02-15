using AICook.Model;
using Microsoft.EntityFrameworkCore;

namespace AICook.Identity.Data;

public class IdentityContext(
	DbContextOptions<IdentityContext> options
) : DbContext(options)
{
	public DbSet<User> Users { get; set; }
	
	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<User>().ToTable("Users");
	}
}