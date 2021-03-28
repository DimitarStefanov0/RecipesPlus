namespace RecipesPlus.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using RecipesPlus.Data.Common.Repositories;
    using RecipesPlus.Data.Models;
    using RecipesPlus.Services.Mapping;
    using RecipesPlus.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly string[] allowedExtensions = new[] { "jpeg", "jpg", "gif", "png" };
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;

        public RecipesService(IDeletableEntityRepository<Recipe> recipesRepository, IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
        }

        public async Task CreateAsync(CreateRecipeInputModel input, string userId, string imagePath)
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

            Directory.CreateDirectory($"{imagePath}/recipes/");

            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');

                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}.");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };

                recipe.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/recipes/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
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

        public T GetById<T>(int id)
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCount()
        {
            return this.recipesRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .To<T>()
                .ToList();
        }

        public async Task UpdateAsync(int id, EditRecipeInputModel input)
        {
            var recipe = this.recipesRepository.All().FirstOrDefault(x => x.Id == id);
            recipe.Name = input.Name;
            recipe.Instructions = input.Instructions;
            recipe.PreparationTime = TimeSpan.FromMinutes(input.PreparationTime);
            recipe.CookingTime = TimeSpan.FromMinutes(input.CookingTime);
            recipe.PortionCount = input.PortionCount;
            recipe.CategoryId = input.CategoryId;

            await this.recipesRepository.SaveChangesAsync();
        }
    }
}
