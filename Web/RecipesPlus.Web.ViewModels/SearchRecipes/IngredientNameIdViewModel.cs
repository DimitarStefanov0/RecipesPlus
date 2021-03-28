namespace RecipesPlus.Web.ViewModels.SearchRecipes
{
    using RecipesPlus.Data.Models;
    using RecipesPlus.Services.Mapping;

    public class IngredientNameIdViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}