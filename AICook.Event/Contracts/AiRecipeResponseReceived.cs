using AICook.Event.Json;

namespace AICook.Event.Contracts;

public record AiRecipeResponseReceived(AiRecipeJsonResponse JsonResponse)
{
}