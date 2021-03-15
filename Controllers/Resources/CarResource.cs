using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vcar.Controllers.Resources
{
    public class CarResource
    {
        public int Id { get; set; }
        public ModelResource Model { get; set; }

        [Required]
        [StringLength(9)]
        public long ContactPhone { get; set; }
        public int year { get; set; }
        public int mileage { get; set; }

    }
}
