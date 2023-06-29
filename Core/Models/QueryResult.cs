using System.Collections.Generic;
using vcar.Core;

namespace vcar.Core.Models
{
    public class QueryResult<T>
    {

        public int Size { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
