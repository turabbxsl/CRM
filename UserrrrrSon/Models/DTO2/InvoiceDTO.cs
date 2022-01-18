using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.DTO2
{
    public class InvoiceDTO
    {
        public string Address { get; set; }
        public string BillTo { get; set; }

        public int BankId { get; set; }
        public int BranchId { get; set; }

        public List<CalcDTO> CalcDTOs { get; set; }

    }

    public class CalcDTO
    {
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
