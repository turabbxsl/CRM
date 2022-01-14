using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.Edit
{
    public class phone_work_edit
    {

        public int PhoneId { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Work { get; set; }
    }
}
