using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.DTO
{
    public class BranchDTO
    {

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string BranchCode { get; set; }

        public string BranchHead { get; set; }

        [Required]
        [MaxLength(300)]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        public string State { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Telephone { get; set; }
        public string AccountName { get; set; }

        public long AccountNo { get; set; }
        public string BankName { get; set; }


    }
}
