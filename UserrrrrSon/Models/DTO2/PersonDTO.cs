using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.models_;

namespace UserrrrrSon.Models.DTO2
{
    public class PersonDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Organization { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public string Pipeline { get; set; }

        [Required]
        public string ExpectedDate { get; set; }

        public string Visible { get; set; }

        public bool ExistingClient { get; set; }

        public string Source { get; set; }

        public string Service { get; set; }

        public bool Priority { get; set; }

        [Required]
        public List<phone_workM> PhonesWorks { get; set; }
        [Required]
        public List<email_workM> EmailsWorks { get; set; }
    }
}
