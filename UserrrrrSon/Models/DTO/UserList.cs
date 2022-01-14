using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.DTO
{
    public class UserList
    {
        public string FullName { get; set; }

        public string Country { get; set; }

        public string BranchName { get; set; }
        public string UserName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Sifre { get; set; }
    }
}
