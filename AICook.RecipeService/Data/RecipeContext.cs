using AICook.Model;
using Microsoft.EntityFrameworkCore;

namespace AICook.RecipeService.Data;

public class RecipeContext(DbContextOptions<RecipeContext> options) : DbContext(options)
{
    public DbSet<RecipeIngredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    // public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<RecipeStep> RecipeSteps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeIngredient>().ToTable("Ingredient");
        modelBuilder.Entity<Recipe>().ToTable("Recipe");
        modelBuilder.Entity<RecipeStep>().ToTable("RecipeStep");
    }
}