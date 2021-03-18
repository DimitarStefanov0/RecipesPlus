namespace RecipesPlus.Data.Models
{
    using System;

    using RecipesPlus.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
