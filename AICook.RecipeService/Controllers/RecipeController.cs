using AICook.Event.Contracts.Recipe;
using AICook.Model.Dto;
using AICook.RecipeService.Data;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
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
        ILogger<RecipeController> logger
    ) : ControllerBase
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
				return Problem(
				    statusCode: StatusCodes.Status404NotFound,
				    detail: "Recipe does not exist!"
				);
            
			return Ok(
			    mapper.Map<RecipeDto>(recipe)
			);
        }
     
        [Authorize]
        [HttpPost("create")]
        [Consumes("application/json")]
        public async Task<ActionResult> CreateJson([FromBody] AiRecipeRequest recipeIdea)
        {
            logger.LogInformation("Sending AiRecipeRequest with prompt: {Prompt}", recipeIdea.Prompt);
            await bus.Publish(recipeIdea);

            return Ok();
        }
    }
}
