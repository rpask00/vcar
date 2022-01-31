using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace vcar.Core.Models

{
    [Table("CarsFeatures")]
    public class CarFeature
    {
        public int CarId { get; set; }
        public int FeatureId { get; set; }
        public Car Car { get; set; }
        public Feature Feature { get; set; }
    }
}
