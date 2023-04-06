using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Modal
{
    public class Blogs
    {
        public int id { get; set; }
        public string blogid { get; set; }
        public string blogtitle { get; set; }
        public string content { get; set; }
        public string metatags { get; set; }
        public string author { get; set; }
        public string blogtitleimage { get; set; }
        public DateTime updateddate { get; set; }       
    }
}
