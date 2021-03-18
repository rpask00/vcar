using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using vcar.Models;

namespace vcar.Controllers.Resources
{
    public class CarResource
    {
        public class Car
        {
            public int Id { get; set; }
            public MakeResource Make { get; set; }
            public ModelResource Model { get; set; }
            public string Email { get; set; }
            public string ContactName { get; set; }
            public int Year { get; set; }
            public bool Registered { get; set; }
            public ICollection<CarFeature> Features { get; set; }

            Car()
            {
                Features = new List<CarFeature>();
            }


        }
    }
}