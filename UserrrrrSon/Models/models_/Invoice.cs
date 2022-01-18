using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.models_
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string BillTo { get; set; }
        public string Date { get; set; }
        public long ReferenceNumber { get; set; }
        public float Total { get; set; }

        public int? BankId { get; set; }
        public Bank Bank { get; set; }

        public int? BranchId { get; set; }
        public Branch Branch { get; set; }

        public List<Calc> Calc { get; set; }
    }
}
