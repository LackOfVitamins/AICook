namespace AICook.Event.Contracts.Recipe;

public record AiRecipeImageUploadRequest(
    int RecipeId,
    byte[] Image
);