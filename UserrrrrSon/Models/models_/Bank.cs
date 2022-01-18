using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.models_
{
    public class Bank
    {
        public int Id { get; set; }

        public string BankName { get; set; }

        public List<Branch> Branches { get; set; }
        public List<Invoice> Invoices{ get; set; }
    }
}
