namespace RecipesPlus.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using RecipesPlus.Services.Data;
    using RecipesPlus.Web.ViewModels.Recipes;
    using RecipesPlus.Web.ViewModels.SearchRecipes;

    public class SearchRecipes : BaseController
    {
        private readonly IRecipesService recipesService;
        private readonly IIngredientsService ingredientsService;

        public SearchRecipes(IRecipesService recipesService, IIngredientsService ingredientsService)
        {
            this.recipesService = recipesService;
            this.ingredientsService = ingredientsService;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchIndexViewModel
            {
                Ingredients = this.ingredientsService.GetAll<IngredientNameIdViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult List(SearchListInputModel input)
        {
            var viewModel = new ListViewModel
            {
                Recipes = this.recipesService.GetByIngredients<RecipeInListViewModel>(input.Ingredients),
            };

            return this.View(viewModel);
        }
    }
}
