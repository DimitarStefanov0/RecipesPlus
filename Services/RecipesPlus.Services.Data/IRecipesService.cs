namespace RecipesPlus.Services.Data
{
    using System.Threading.Tasks;

    using RecipesPlus.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipeInputModel input);
    }
}
