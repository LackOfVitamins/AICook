using AICook.RecipeService.Models.DTO;
using AutoMapper;

namespace AICook.RecipeService.Models.Profiles;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Recipe, RecipeDto>();
        CreateMap<RecipeStep, RecipeStepDto>();
        CreateMap<RecipeIngredient, RecipeIngredientDto>();
    }
}