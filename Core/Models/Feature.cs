using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vcar.Core.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CarFeature> Features { get; set; }

        Feature() { 
            Features = new List<CarFeature>();
        }

    }
}
