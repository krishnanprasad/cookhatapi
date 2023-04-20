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
        public string recipetypeimgsrc { get; set; }
        public int recipetypeid { get; set; }
        public string recipetypename { get; set; }
        public string recipeimage{ get; set; }
        public List<Ingredient> recipeingredients { get; set; }
        public List<Steps> recipesteps { get; set; }
        public string recipevideosrc { get; set; }
        public string chefid { get; set; }
        public string chefname { get; set; }
        public string chefimgurl { get; set; }
        public DateTimeOffset? recipecreateddate { get; set; }
        public int totalrecipes { get; set; }
    }
    public class RecipeChef : RecipeDetail
    {
        public int totalfollowers { get; set; }
        public int totalrecipes { get; set; }
    }
    public class RecipeFilter
    {
        public string text { get; set; }
        public List<string> ingredients{ get; set; }

        public List<Category> category { get; set; }
        public List<string> cuisinetype { get; set; }
    }
}
