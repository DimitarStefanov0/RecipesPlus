namespace RecipesPlus.Services.Data
{
    using System.Linq;

    using RecipesPlus.Data.Common.Repositories;
    using RecipesPlus.Data.Models;
    using RecipesPlus.Services.Data.Models;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;

        public GetCountsService(IDeletableEntityRepository<Recipe> recipesRepository, IDeletableEntityRepository<Category> categoriesRepository, IDeletableEntityRepository<Ingredient> ingredientsRepository, IDeletableEntityRepository<Image> imagesRepository)
        {
            this.recipesRepository = recipesRepository;
            this.categoriesRepository = categoriesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.imagesRepository = imagesRepository;
        }

        public CountsDto GetCounts()
        {
            var data = new CountsDto
            {
                RecipesCount = this.recipesRepository.AllAsNoTracking().Count(),
                CategoriesCount = this.categoriesRepository.AllAsNoTracking().Count(),
                IngredientsCount = this.ingredientsRepository.AllAsNoTracking().Count(),
                ImagesCount = this.imagesRepository.AllAsNoTracking().Count(),
            };

            return data;
        }
    }
}
