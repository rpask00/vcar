using System.Collections.Generic;

namespace vcar.Controllers.Resources
{
    public class QueryResultResource<T>
    {
        public int Size { get; set; }
        public IEnumerable<T> Items { get; set; }

    }
}