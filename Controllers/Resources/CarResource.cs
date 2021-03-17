using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace vcar.Controllers.Resources
{
    public class Contact
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
    public class CarResource
    {
        public Contact contact { get; set; }

        [Required]
        public int ModelId { get; set; }

        public int year { get; set; }
        public bool registered { get; set; }
        public ICollection<int> Features { get; set; }


    }
}
