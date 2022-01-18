using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.models_
{
    public class Calculate
    {
        public int Id { get; set; }
        public int Total { get; set; }
        public List<Calc> Calcs { get; set; }
        public Invoice Invoice { get; set; }
    }

    public class Calc
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }

}
