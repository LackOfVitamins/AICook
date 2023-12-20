namespace AICook.Event.Contracts;

public record AiRecipeImageGenerationRequest(
    int RecipeId,
    string Prompt
    )
{
}