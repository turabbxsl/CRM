using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.DTO2
{
    public class UserProfileDTO
    {
        [Required]       
        public int UserId { get; set; }
    }
}
