namespace RecipesPlus.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipesPlus.Services.Data;
    using RecipesPlus.Web.ViewModels.Recipes;

    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;

        public RecipesController(ICategoriesService categoriesService, IRecipesService recipesService)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.recipesService.CreateAsync(input, userId);

            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            const int itemsPerPage = 12;
            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Recipes = this.recipesService.GetAll<RecipeInListViewModel>(id, itemsPerPage),
                RecipesCount = this.recipesService.GetCount(),
            };

            if (id <= 0 || id > viewModel.PagesCount)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
