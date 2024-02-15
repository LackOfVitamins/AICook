namespace AICook.Model.Dto;

public record RecipeDto(
	int Id,
	string Title,
	string IntroText,
	string ImageUrl,
	ICollection<RecipeStepDto> Steps,
	ICollection<RecipeIngredientDto> Ingredients
);
public record RecipeListItemDto(
	int Id,
	string Title,
	string ImageUrl
);

public record RecipeIngredientDto(
	string Name,
	string Quantity
);

public record RecipeStepDto(
	int StepNumber,
	string StepText
);
