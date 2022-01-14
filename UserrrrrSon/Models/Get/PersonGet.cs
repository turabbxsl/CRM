using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.models_;

namespace UserrrrrSon.Models.Get
{
    public class PersonGet
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

        public List<ph> Phones { get; set; }
        public List<em> Emails { get; set; }



    }

    public class ph
    {
        public string Number { get; set; }
        public string Work { get; set; }
    }
    public class em
    {
        public string Email { get; set; }
        public string Work { get; set; }
    }
}
