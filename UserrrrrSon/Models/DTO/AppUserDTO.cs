using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models
{
    public class AppUserDTO
    {

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Sifre { get; set; }

    }
}
