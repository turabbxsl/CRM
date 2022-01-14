using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.DTO2
{
    public class phone_workM
    {
        [Phone]
        public string PhoneNumber { get; set; }
        public string Work { get; set; }
    }
}
