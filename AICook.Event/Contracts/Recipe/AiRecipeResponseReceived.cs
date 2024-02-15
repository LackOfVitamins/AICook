using AICook.Model.Json;

namespace AICook.Event.Contracts.Recipe;

public record AiRecipeResponseReceived(AiRecipeJsonResponse JsonResponse);