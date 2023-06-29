using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace vcar.Controllers.Resources
{

    public class SaveCarResource
    {
        public int Id { get; set; }
        public ContactResource Contact { get; set; }

        [Required]
        public int ModelId { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public bool Registered { get; set; }
        public ICollection<int> Features { get; set; }


    }
}

