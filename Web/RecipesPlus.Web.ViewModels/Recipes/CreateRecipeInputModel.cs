namespace RecipesPlus.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateRecipeInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(50)]
        public string Instructions { get; set; }

        [Range(1, 60 * 24)]
        [Display(Name = "Preparation time (in minutes)")]
        public int PreparationTime { get; set; }

        [Range(1, 60 * 24)]
        [Display(Name = "Cooking time (in minutes)")]
        public int CookingTime { get; set; }

        [Range(1, 100)]
        public int PortionCount { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<RecipeIngredientInputModel> Ingredients { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }
    }
}
