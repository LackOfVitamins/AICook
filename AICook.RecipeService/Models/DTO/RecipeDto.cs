namespace AICook.RecipeService.Models.DTO;

public record RecipeDto(
    int Id,
    string Title,
    string IntroText,
    string ImageUrl,
    bool Visible,
    ICollection<RecipeStepDto> Steps,
    ICollection<RecipeIngredientDto> Ingredients)
{}

public record RecipeIngredientDto(
    string Name,
    string Quantity
    )
{}

public record RecipeStepDto(
    int StepNumber,
    string StepText
)
{}

