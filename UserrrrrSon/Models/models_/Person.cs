using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.models_
{
    public class Person
    {

        public int PersonId { get; set; }

        public string Name { get; set; }

        public string Organization { get; set; }

        public string Title { get; set; }

        public double Value { get; set; }

        public string Pipeline { get; set; }

        public string ExpectedDate { get; set; }

        public string Visible { get; set; }

        public int ReferenceNumber { get; set; }

        public bool ExistingClient { get; set; }

        public string Source { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public bool Priority { get; set; }

        public List<Phone> Phones { get; set; }


    }
}
