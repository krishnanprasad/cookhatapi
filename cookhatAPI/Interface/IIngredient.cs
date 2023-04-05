using cookhatAPI.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Interface
{
    public interface IIngredient
    {
        List<Ingredient> GetRecommendedIngredientList();
        List<Ingredient> GetIngredientListSearch(string? searchterm);
    }
}
