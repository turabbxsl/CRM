using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.models_;

namespace UserrrrrSon.Models.Authentication
{
    public class AppUser : IdentityUser<int>
    {

        public string FullName { get; set; }

        public string Country { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }


        public int? UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }



    }
}
