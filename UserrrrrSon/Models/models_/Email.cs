using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.models_
{
    public class Email
    {
        public int EmailID { get; set; }

        public string Emaill { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int WorkId { get; set; }
        public Work Work { get; set; }
    }
}
