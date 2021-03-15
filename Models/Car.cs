using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vcar.Models
{
    public class Car
    {
        public int Id { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }

        [Required]
        [StringLength(9)]
        public long ContactPhone { get; set; }
        public int year { get; set; }
        public int mileage { get; set; }


    }
}
