using cookhatAPI.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Interface
{
    public interface IRecipe
    {
        RecipeDetail GetRecipeDetail(string recipeID);
        bool AddRecipe(AddRecipe recipe);
        List<RecipeChef> GetRecipeList(string? infid);
        List<RecipeChef> GetTrendingRecipeList();
        List<RecipeDetail> GetRecipeSearchList(string recipesearch,string ingredient,string category);
    }
}
