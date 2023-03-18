using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Modal
{
    public class RecipeDetail
    {
        public string recipeid { get; set; }
        public Chef ChefDetail { get; set; }
        public string recipename { get; set; }
        public int recipetime { get; set; }
        public string recipecuisine { get; set; }
        public string recipefoodtime { get; set; }
        public List<Ingredient> recipeingredients { get; set; }
        public List<Steps> recipesteps { get; set; }
        public string recipevideosrc { get; set; }
        public string chefid { get; set; }
        public string chefname { get; set; }
        public string chefimgurl { get; set; }
        public DateTimeOffset? recipecreateddate { get; set; }
    }
}
