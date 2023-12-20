
using AICook.RecipeService.Models;
using Microsoft.EntityFrameworkCore;

namespace AICook.RecipeService.Data;

public class RecipeContext : DbContext
{
    public DbSet<RecipeIngredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    // public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<RecipeStep> RecipeSteps { get; set; }

    public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeIngredient>().ToTable("Ingredient");
        modelBuilder.Entity<Recipe>().ToTable("Recipe");
        modelBuilder.Entity<RecipeStep>().ToTable("RecipeStep");
    }
}