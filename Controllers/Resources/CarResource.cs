using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using vcar.Core.Models;

namespace vcar.Controllers.Resources
{
    public class CarResource
    {
        public int Id { get; set; }
        public MakeResource Make { get; set; }
        public ModelResource Model { get; set; }
        public ContactResource Contact { get; set; }
        public int Price {get;set;}
        public int Year { get; set; }
        public bool Registered { get; set; }
        public ICollection<FeatureResource> Features { get; set; }
        public DateTime lasUpdate { get; set; }
        public string thumbnail { get; set; }

        CarResource()
        {
            Features = new List<FeatureResource>();
        }


    }
}
