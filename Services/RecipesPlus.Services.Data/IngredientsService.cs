namespace RecipesPlus.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using RecipesPlus.Data.Common.Repositories;
    using RecipesPlus.Data.Models;
    using RecipesPlus.Services.Mapping;

    public class IngredientsService : IIngredientsService
    {
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;

        public IngredientsService(IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.ingredientsRepository.AllAsNoTracking().To<T>().ToList();
        }
    }
}
