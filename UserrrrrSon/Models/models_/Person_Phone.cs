using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.models_
{
    public class Person_Phone
    {

        public int Person_PhoneId { get; set; }

        public int PhoneId { get; set; }
        public List<Phone> Phones { get; set; }

        public int WorkId { get; set; }
        public List<Work> Works { get; set; }



    }
}
