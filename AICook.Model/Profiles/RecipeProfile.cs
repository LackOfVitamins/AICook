using AICook.Model.Dto;
using AutoMapper;

namespace AICook.Model.Profiles;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Recipe, RecipeDto>();
        CreateMap<Recipe, RecipeListItemDto>();
        CreateMap<RecipeStep, RecipeStepDto>();
        CreateMap<RecipeIngredient, RecipeIngredientDto>();
    }
}