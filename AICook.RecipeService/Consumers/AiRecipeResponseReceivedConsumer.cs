using AICook.Event.Contracts.Recipe;
using AICook.Model;
using AICook.RecipeService.Data;
using MassTransit;

namespace AICook.RecipeService.Consumers;

public class AiRecipeResponseReceivedConsumer(
    RecipeContext recipeContext,
    IBus bus, 
    ILogger<AiRecipeResponseReceivedConsumer> logger) 
    : IConsumer<AiRecipeResponseReceived>
{
    public async Task Consume(ConsumeContext<AiRecipeResponseReceived> context)
    {
        logger.LogInformation("Received AiRecipeResponseReceived!");
        var jsonResponse = context.Message.JsonResponse;
        var recipe = Recipe.FromJsonResponse(jsonResponse);
        
        logger.LogInformation("Adding recipe to database!");
        var recipeEntry = await recipeContext.Recipes.AddAsync(recipe);
        await recipeContext.SaveChangesAsync();
        
        await bus.Publish(new AiRecipeImageGenerationRequest(recipeEntry.Entity.Id, jsonResponse.Dp));
    }
}