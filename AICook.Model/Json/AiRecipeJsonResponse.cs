namespace AICook.Model.Json;

/// <summary>
/// The JSON object for the recipe
/// </summary>
/// <param name="Tt">Title</param>
/// <param name="In">IntroText</param>
/// <param name="Dp">DallePrompt</param>
/// <param name="St">Steps</param>
/// <param name="Ig">Ingredients</param>
public record AiRecipeJsonResponse(
	string Tt,
	string In,
	string Dp,
	IList<AiRecipeJsonResponseRecipeStep> St,
	IList<AiRecipeJsonResponseRecipeIngredient> Ig
);

/// <summary>
/// The JSON object for the recipe step
/// </summary>
/// <param name="Nb">StepNumber</param>
/// <param name="Tx">StepText</param>
public record AiRecipeJsonResponseRecipeStep(
	int Nb,
	string Tx
);

/// <summary>
/// The JSON object for the recipe ingriedient
/// </summary>
/// <param name="Nm">Name</param>
/// <param name="Qn">Quantity</param>
public record AiRecipeJsonResponseRecipeIngredient(
	string Nm,
	string Qn
);

