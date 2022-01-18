using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.Authentication;

namespace UserrrrrSon.Models.models_
{
    public class Branch
    {

        [Key]
        public int BranchId { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string BranchCode { get; set; }

        public string BranchHead { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        public string State { get; set; }

        [Required]
        public string City { get; set; }

        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Telephone { get; set; }

        public string AccountName { get; set; }
        public long AccountNo { get; set; }

        public int BankId { get; set; }
        public Bank Bank { get; set; }


        public List<Invoice> Invoices { get; set; }

        public List<AppUser> AppUsers { get; set; }


    }
}
