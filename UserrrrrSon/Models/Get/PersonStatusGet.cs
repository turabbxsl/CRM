using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.Get;

namespace UserrrrrSon.Models.DTO2
{
    public class PersonStatusGet
    {
        public int ID { get; set; }
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

        public string Service { get; set; }
        public string Status { get; set; }

        public bool Priority { get; set; }

        public List<ph> Phones { get; set; }
        public List<em> Emails { get; set; }
    }
}
