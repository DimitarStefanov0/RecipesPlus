// ReSharper disable VirtualMemberCallInConstructor
namespace RecipesPlus.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using RecipesPlus.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Recipes = new HashSet<Recipe>();
            this.Images = new HashSet<Image>();
            this.Votes = new HashSet<Vote>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
