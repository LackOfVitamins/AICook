namespace AICook.Event.Contracts.Recipe;

public record AiRecipeImageGenerationRequest(
    int RecipeId,
    string Prompt
);