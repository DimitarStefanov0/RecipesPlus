namespace RecipesPlus.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using RecipesPlus.Data.Models;
    using RecipesPlus.Services.Mapping;

    public class SingleRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AddedByUserUserName { get; set; }

        public string Instructions { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan CookingTime { get; set; }

        public int PortionCount { get; set; }

        public virtual IEnumerable<IngredientsViewModel> Ingredients { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, SingleRecipeViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(r => r.Images.FirstOrDefault().RemoteImageUrl != null ?
                    r.Images.FirstOrDefault().RemoteImageUrl : "/images/recipes/" + r.Images.FirstOrDefault().Id + "." + r.Images.FirstOrDefault().Extension));
        }
    }
}
