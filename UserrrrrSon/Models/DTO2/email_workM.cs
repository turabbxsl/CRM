using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UserrrrrSon.Models.DTO2
{
    public class email_workM
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Work { get; set; }
    }
}
