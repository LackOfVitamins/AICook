namespace AICook.Event.Contracts;

public record AiRecipeImageUploadRequest(
    int RecipeId,
    byte[] Image)
{
}