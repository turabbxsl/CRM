using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.DTO2;

namespace UserrrrrSon.Models.Edit
{
    public class PersonEditDTO
    {
        public string Name { get; set; }
        public string Organization { get; set; }
        public string Title { get; set; }
        public double Value { get; set; }
        public string Pipeline { get; set; }
        public string ExpectedDate { get; set; }
        public string Visible { get; set; }

        public bool ExistingClient { get; set; }

        public string Source { get; set; }

        public string Service { get; set; }

        public bool Priority { get; set; }

        public List<phone_work_edit> PhoneNumbers { get; set; }
        public List<email_work_edit> Emails { get; set; }

    }
}
