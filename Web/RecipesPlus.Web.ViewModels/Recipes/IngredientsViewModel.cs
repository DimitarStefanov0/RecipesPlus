namespace RecipesPlus.Web.ViewModels.Recipes
{
    using RecipesPlus.Data.Models;
    using RecipesPlus.Services.Mapping;

    public class IngredientsViewModel : IMapFrom<RecipeIngredient>
    {
        public string IngredientName { get; set; }

        public string Quantity { get; set; }
    }
}
