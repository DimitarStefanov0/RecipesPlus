namespace RecipesPlus.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using RecipesPlus.Data.Models;
    using RecipesPlus.Services.Mapping;

    public class IndexPageRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, IndexPageRecipeViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(r => r.Images.FirstOrDefault().RemoteImageUrl != null ?
                    r.Images.FirstOrDefault().RemoteImageUrl : "/images/recipes/" + r.Images.FirstOrDefault().Id + "." + r.Images.FirstOrDefault().Extension));
        }
    }
}
