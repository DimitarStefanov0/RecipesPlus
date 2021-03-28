namespace RecipesPlus.Web.ViewModels.SearchRecipes
{
    using System.Collections.Generic;

    using RecipesPlus.Web.ViewModels.Recipes;

    public class ListViewModel
    {
        public IEnumerable<RecipeInListViewModel> Recipes { get; set; }
    }
}
