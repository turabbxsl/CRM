using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.Get
{
    public class InvoiceGet
    {
        public string Address { get; set; }
        public string BillTo { get; set; }
        public string Date { get; set; }
        public long ReferenceNumber { get; set; }
        public float Total { get; set; }
        public string Branch { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string Bank { get; set; }
        public List<CalcGet> calcGets { get; set; }

    }
    public class CalcGet
    {

        public int Quantity { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }

    }
}
