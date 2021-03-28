namespace RecipesPlus.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateRecipeInputModel : BaseRecipeInputModel
    {
        public IEnumerable<IFormFile> Images { get; set; }

        [Required(ErrorMessage = "Ingredients are required")]
        public IEnumerable<RecipeIngredientInputModel> Ingredients { get; set; }
    }
}
