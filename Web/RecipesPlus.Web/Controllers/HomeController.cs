namespace RecipesPlus.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using RecipesPlus.Services.Data;
    using RecipesPlus.Web.ViewModels;
    using RecipesPlus.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService countsService;
        private readonly IRecipesService recipesService;

        public HomeController(IGetCountsService countsService, IRecipesService recipesService)
        {
            this.countsService = countsService;
            this.recipesService = recipesService;
        }

        public IActionResult Index()
        {
            var countsDto = this.countsService.GetCounts();
            var viewModel = new IndexViewModel
            {
                RecipesCount = countsDto.RecipesCount,
                CategoriesCount = countsDto.CategoriesCount,
                IngredientsCount = countsDto.IngredientsCount,
                ImagesCount = countsDto.ImagesCount,
                RandomRecipes = this.recipesService.GetRandom<IndexPageRecipeViewModel>(10),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
