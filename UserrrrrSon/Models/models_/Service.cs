using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.models_
{
    public class Service
    {
        public int ServiceId { get; set; }

        public string Name { get; set; }

        public List<Person> Persons { get; set; }
    }
}
