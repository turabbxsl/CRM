using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.models_
{
    public class Phone
    {

        public int PhoneID { get; set; }

        public string PhoneNumber { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int WorkId { get; set; }
        public Work Work { get; set; }
    }
}
