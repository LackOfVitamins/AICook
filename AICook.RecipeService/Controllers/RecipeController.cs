using AICook.Event.Contracts;
using AICook.RecipeService.Data;
using AICook.RecipeService.Models;
using AICook.RecipeService.Models.DTO;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AICook.RecipeService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecipeController(
        RecipeContext context, 
        IMapper mapper, 
        IBus bus, 
        ILogger<RecipeController> logger) 
        : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeListItemDto>>> GetRecipes()
        {
            return await context.Recipes
                .Where(r => r.Visible == true)
                .Include(r => r.Steps)
                .Include(r => r.Ingredients)
                .Select(r => mapper.Map<RecipeListItemDto>(r))
                .ToListAsync();
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(int id)
        {
            var recipe = await context.Recipes
                .Where(r => r.Visible == true)
                .Include(r => r.Steps)
                .Include(r => r.Ingredients)
                .SingleOrDefaultAsync(r => r.Id == id);
            
            if (recipe == null)
            {
                return NotFound();
            }
            
            return mapper.Map<RecipeDto>(recipe);
        }
        
        [HttpPost("create")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateJson([FromBody] AiRecipeRequest recipeIdea)
        {
            logger.LogInformation("Sending AiRecipeRequest with prompt: {Prompt}", recipeIdea.Prompt);
            await bus.Publish(recipeIdea);

            return Ok();
        }
    }
}
