namespace RecipesPlus.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RecipesPlus.Data.Common.Repositories;
    using RecipesPlus.Data.Models;
    using RecipesPlus.Services.Mapping;
    using RecipesPlus.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;

        public RecipesService(IDeletableEntityRepository<Recipe> recipesRepository, IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
        }

        public async Task CreateAsync(CreateRecipeInputModel input, string userId)
        {
            var recipe = new Recipe
            {
                Name = input.Name,
                Instructions = input.Instructions,
                PreparationTime = TimeSpan.FromMinutes(input.PreparationTime),
                CookingTime = TimeSpan.FromMinutes(input.CookingTime),
                CategoryId = input.CategoryId,
                PortionCount = input.PortionCount,
                AddedByUserId = userId,
            };

            foreach (var inputIngredient in input.Ingredients)
            {
                var ingredient = this.ingredientsRepository
                    .All()
                    .FirstOrDefault(x => x.Name == inputIngredient.IngredientName);

                if (ingredient == null)
                {
                    ingredient = new Ingredient
                    {
                        Name = inputIngredient.IngredientName,
                    };
                }

                recipe.Ingredients.Add(new RecipeIngredient
                {
                    Ingredient = ingredient,
                    Quantity = inputIngredient.Quantity,
                });
            }

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public int GetCount()
        {
            return this.recipesRepository.AllAsNoTracking().Count();
        }
    }
}
