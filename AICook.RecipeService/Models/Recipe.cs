using AICook.Event.Json;

namespace AICook.RecipeService.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string IntroText { get; set; }
    public string? ImageUrl { get; set; }
    public bool Visible { get; set; }

    public ICollection<RecipeIngredient> Ingredients { get; init; } = new List<RecipeIngredient>();
    public ICollection<RecipeStep> Steps { get; init; } = new List<RecipeStep>();

    public static Recipe FromJsonResponse(AiRecipeJsonResponse response)
    {
        return new Recipe
        {
            Title = response.Tt,
            IntroText = response.In,
            Steps = response.St
                .Select(RecipeStep.FromJsonResponse)
                .ToList(),
            Ingredients = response.Ig.
                Select(RecipeIngredient.FromJsonResponse)
                .ToList()
        };
    }
}