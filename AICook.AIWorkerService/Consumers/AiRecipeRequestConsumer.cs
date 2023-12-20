using System.Text.Json;
using System.Text.RegularExpressions;
using AICook.Event.Contracts;
using AICook.Event.Json;
using MassTransit;
using OpenAI_API;
using OpenAI_API.Models;

namespace AICook.AIWorkerService.Consumers;

public class AiRecipeRequestConsumer(IOpenAIAPI openAiApi, IBus bus, ILogger<AiRecipeRequestConsumer> logger) : IConsumer<AiRecipeRequest>
{
    public async Task Consume(ConsumeContext<AiRecipeRequest> context)
    {
        var prompt = context.Message.Prompt;
        logger.LogInformation("Consuming AiRecipeRequest! Generating recipe with prompt: {Prompt}", prompt);
        
        // Setting up model
        var conversation = openAiApi.Chat.CreateConversation();
        conversation.Model = Model.GPT4_Turbo;
        conversation.RequestParameters.Temperature = 0;
        conversation.RequestParameters.MaxTokens = 2048;

        // Appending messages to send
        conversation.AppendMessage(ChatGptPrompts.RecipeSystemMessage);
        conversation.AppendMessage(ChatGptPrompts.RecipeIdeaChatMessage(prompt));
        
        var response = await conversation.GetResponseFromChatbotAsync();
        logger.LogInformation("Received response from OpenAI: {Response}", response);
        
        try
        {
            // Removing potential markdown code block tags
            response = Regex.Replace(response, "`{3}([\\w]*)", "");
            
            // Deserializing the response from OpenAI (GPT)
            var jsonResponse = JsonSerializer.Deserialize<AiRecipeJsonResponse>(response)!;
            await bus.Publish(new AiRecipeResponseReceived(jsonResponse));
        }
        catch (Exception e)
        {
            logger.LogError("Deserializing response failed! Exception: {Exception}", e.Message);
        }
    }
}