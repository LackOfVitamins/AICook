namespace AICook.Event.Contracts.Recipe;

public record AiRecipeImageUploaded(
    int RecipeId,
    string ImageUrl
);