namespace RecipesPlus.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using RecipesPlus.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category { Name = "Салати" });
            await dbContext.Categories.AddAsync(new Category { Name = "Десерти" });
            await dbContext.Categories.AddAsync(new Category { Name = "Предястия" });
            await dbContext.Categories.AddAsync(new Category { Name = "Ястия с месо" });
            await dbContext.Categories.AddAsync(new Category { Name = "Ястия с риба" });
            await dbContext.Categories.AddAsync(new Category { Name = "Вегетариански ястия" });
            await dbContext.Categories.AddAsync(new Category { Name = "Печива" });
            await dbContext.Categories.AddAsync(new Category { Name = "Напитки" });

            await dbContext.SaveChangesAsync();
        }
    }
}
