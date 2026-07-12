using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project19_RecipeSuggestionAI.Models;

namespace NetCoreAI.Project19_RecipeSuggestionAI.Controllers
{
    public class RecipeController : Controller
    {
        private readonly AIService _aiService;

        public RecipeController(AIService aiService)
        {
            _aiService = aiService;
        }

        [HttpGet]
        public IActionResult CreateRecipe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe(string ingredients)
        {
            var result = await  _aiService.GetRecipeAsync(ingredients);
            ViewBag.recipe = result;
            return View();
        }
    }
}
