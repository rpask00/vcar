using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using vcar.Extensions;

namespace vcar.Core.Models
{
    public class CarQuery : IQueryObj
    {
        public int? ModelId { get; set; }
        public int? MakeId { get; set; }
        public int? yearmax { get; set; }
        public int? yearmin { get; set; }
        public string sortBy { get; set; }
        public bool sortAsc { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? PriceMin { get; set; }
        public int? PriceMax { get; set; }
        public string Owner { get; set; }
    }
}