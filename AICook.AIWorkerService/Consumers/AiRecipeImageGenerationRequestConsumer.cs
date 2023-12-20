using System.Runtime.InteropServices.JavaScript;
using AICook.Event.Contracts;
using Bytewizer.Backblaze.Client;
using MassTransit;
using OpenAI_API;
using OpenAI_API.Images;
using OpenAI_API.Models;
using RandomString4Net;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

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
            Model.DALLE2,
            ImageSize._1024,
            responseFormat: ImageResponseFormat.B64_json
        );
        
        var result = await openAiApi.ImageGenerations.CreateImageAsync(imageRequest);
        var bytes = Convert.FromBase64String(result.Data[0].Base64Data);

        await bus.Publish(new AiRecipeImageUploadRequest(recipeId, bytes));
    }
}