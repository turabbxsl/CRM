using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.Authentication;

namespace UserrrrrSon.Models.models_
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Image { get; set; }

        public AppUser AppUser { get; set; }

    }
}
