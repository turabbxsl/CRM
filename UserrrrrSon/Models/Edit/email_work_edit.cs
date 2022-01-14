using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.Edit
{
    public class email_work_edit
    {


        public int EmailId { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Work { get; set; }

    }
}
