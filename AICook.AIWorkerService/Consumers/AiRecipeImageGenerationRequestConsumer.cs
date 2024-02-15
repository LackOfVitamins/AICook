using AICook.Event.Contracts.Recipe;
using MassTransit;
using OpenAI_API;
using OpenAI_API.Images;

namespace AICook.AIWorkerService.Consumers;

public class AiRecipeImageGenerationRequestConsumer(
    IOpenAIAPI openAiApi,
    IBus bus, 
    ILogger<AiRecipeImageGenerationRequestConsumer> logger)
    : IConsumer<AiRecipeImageGenerationRequest>
{
    public async Task Consume(ConsumeContext<AiRecipeImageGenerationRequest> context)
    {
        var prompt = context.Message.Prompt;
        var recipeId = context.Message.RecipeId;
        logger.LogInformation(
            "Consuming AiRecipeImageGenerationRequest! Generation image with prompt: {Prompt} for recipe with id: {RecipeId}",
            prompt,
            recipeId
        );

        var imageRequest = new ImageGenerationRequest(
            prompt,
            OpenAI_API.Models.Model.DALLE2,
            ImageSize._1024,
            responseFormat: ImageResponseFormat.B64_json
        );
        
        var result = await openAiApi.ImageGenerations.CreateImageAsync(imageRequest);
        var bytes = Convert.FromBase64String(result.Data[0].Base64Data);

        await bus.Publish(new AiRecipeImageUploadRequest(recipeId, bytes));
    }
}