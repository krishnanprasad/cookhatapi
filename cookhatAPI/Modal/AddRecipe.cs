using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Modal
{
    public class AddRecipe
    {
        public string influencerid { get; set; }
        public string recipename { get; set; }
        public string duration { get; set; }
        public string cuisinetype { get; set; }
        public string videourl { get; set; }
        public List<Ingredient> ingredient { get; set; }
        public List<Steps> steps { get; set; }
        public string type{ get; set; }
    }
}
