using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.models_
{
    public class Work
    {

        public int WorkId { get; set; }

        public string Name { get; set; }

        public List<Email> Email { get; set; }
        public List<Phone> Phone { get; set; }

    }
}
