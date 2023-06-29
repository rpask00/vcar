using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vcar.Core.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public int ModelId { get; set; }
        public int Price { get; set; }
        public Model Model { get; set; }
        [Required]
        [StringLength(30)]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string ContactName { get; set; }
        public int year { get; set; }
        public bool registered { get; set; }
        public ICollection<CarFeature> Features { get; set; }
        public DateTime lasUpdate { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public string thumbnail { get; set; }

        Car()
        {
            Features = new List<CarFeature>();
            Photos = new List<Photo>();
        }


    }
}

