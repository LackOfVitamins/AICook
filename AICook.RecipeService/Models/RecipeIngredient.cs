using AICook.Event.Json;
using Microsoft.EntityFrameworkCore;

namespace AICook.RecipeService.Models;

public class RecipeIngredient 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Quantity { get; set; }
    public Recipe Recipe { get; set; }

    public static RecipeIngredient FromJsonResponse(AiRecipeJsonResponseRecipeIngredient ingredient)
    {
        return new RecipeIngredient
        {
            Name = ingredient.Nm,
            Quantity = ingredient.Qn
        };
    } 
}