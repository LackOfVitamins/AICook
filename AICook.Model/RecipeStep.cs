using AICook.Model.Json;

namespace AICook.Model;

public class RecipeStep
{
    public int Id { get; set; }
    public Recipe Recipe { get; set; }
    public int StepNumber { get; set; }
    public string StepText { get; set; }

    public static RecipeStep FromJsonResponse(AiRecipeJsonResponseRecipeStep step)
    {
        return new RecipeStep
        {
            StepNumber = step.Nb, 
            StepText = step.Tx
        };
    }
}