using System;
using System.Collections.Generic;
using DLL.Model.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DLL.Model
{
    public class AppUser : IdentityUser<int>,ITrackable,ISoftDeletable
    {
        public string FullName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }

        public virtual ICollection<IdentityUserClaim<int>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<int>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<int>> Tokens { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}