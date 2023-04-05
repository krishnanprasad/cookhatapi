using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Modal
{
    public class Category
    {
        public string groupname { get; set; }
        public List<CategoryItems> items { get; set; }

    }

    public class CategoryItems
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool @checked { get; set; }
    }
}
