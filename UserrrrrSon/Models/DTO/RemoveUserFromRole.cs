﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.DTO
{
    public class RemoveUserFromRole
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
