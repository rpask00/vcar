using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace vcar.Controllers.Resources
{
    public class FilterResource
    {
        public int? ModelId { get; set; }
        public int? MakeId { get; set; }
        public int? yearmax { get; set; }
        public int? yearmin { get; set; }
        public ICollection<int> Features { get; set; }
    }
}