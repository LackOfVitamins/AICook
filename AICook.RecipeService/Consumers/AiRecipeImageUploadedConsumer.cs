using AICook.Event.Contracts;
using AICook.RecipeService.Data;
using MassTransit;

namespace AICook.RecipeService.Consumers;

public class AiRecipeImageUploadedConsumer(
    RecipeContext recipeContext,
    ILogger<AiRecipeImageUploadedConsumer> logger)
    : IConsumer<AiRecipeImageUploaded>
{
    public async Task Consume(ConsumeContext<AiRecipeImageUploaded> context)
    {
        var recipeId = context.Message.RecipeId;
        var imageUrl = context.Message.ImageUrl;
        logger.LogInformation("Consuming AiRecipeImageUploaded! RecipeId: {RecipeId}, FileName: {FileName}", recipeId, imageUrl);

        try
        {
            var recipe = await recipeContext.Recipes.FindAsync(recipeId);
            
            if (recipe == null) {
                logger.LogError("Updating recipe failed! Recipe not found in db.");
                return;
            }

            recipe.ImageUrl = imageUrl;
            recipe.Visible = true;
            await recipeContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError("Updating recipe failed! Exception: {Exception}", e);
        }
    }
}