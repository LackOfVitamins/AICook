namespace AICook.Event.Contracts;

public record AiRecipeImageUploaded(
    int RecipeId,
    string ImageUrl)
{
}