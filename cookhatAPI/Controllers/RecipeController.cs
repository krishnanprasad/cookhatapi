using cookhatAPI.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cookhatAPI.Interface;
using Microsoft.Extensions.DependencyInjection;
using cookhatAPI.DAL;
using cookhatAPI.Connection;
using System.IO;
using Newtonsoft.Json;

namespace cookhatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController
    {
        //private readonly IDbConnectionFactory _sqlConnection;
        private IRecipe _recipeRepo;


        [ActivatorUtilitiesConstructor]

        //public RecipeController(IDbConnectionFactory sqlConnection)
        //{
        //    _sqlConnection = sqlConnection;
        //}
        public RecipeController(IRecipe recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }
        [HttpGet]
        [Route("GetRecipeDetail")]

        public ActionResult<RecipeDetail> GetRecipeDetail(string id)
        {
            string recipeid = id;
            RecipeDetail _recipe = _recipeRepo.GetRecipeDetail(id);
            return new OkObjectResult(_recipe);
        }
        [HttpPost]
        [Route("AddRecipe")]

        public ActionResult<bool> AddRecipe([FromBody] AddRecipe recipe)
        {
            AddRecipe request = recipe;
            bool IsAddRecipeSuccess = _recipeRepo.AddRecipe(request);
            return new OkObjectResult(IsAddRecipeSuccess);
        }
        [HttpGet]
        [Route("GetRecipeList")]

        public ActionResult<RecipeChef> GetRecipeList(string? chef_id)
        {
            string chefid = chef_id;
            if (chefid == null || chefid=="")
            {
                chefid =null;
            }
            List<RecipeChef> _recipeList = _recipeRepo.GetRecipeList(chefid);
            return new OkObjectResult(_recipeList);
        }
        [HttpPost]
        [Route("GetSearchRecipeList")]

        public ActionResult<RecipeChef> GetRecipeSearchList([FromBody] RecipeFilter searchRecipe)
        {
            RecipeFilter o_searchrecipe = searchRecipe;

            string o_ingredients = "";
            string o_category = "";
            string o_search = "";
            if (o_searchrecipe.ingredients.Count > 0)
            {
                o_searchrecipe.ingredients = o_searchrecipe.ingredients.Distinct().ToList();
                foreach (var single_ingredient in o_searchrecipe.ingredients)
                {
                    o_ingredients = o_ingredients+single_ingredient + ",";
                }
            }
            if (o_searchrecipe.category.Count > 0)
            {
                o_searchrecipe.category = o_searchrecipe.category.Distinct().ToList();
                foreach (Category cat in o_searchrecipe.category)
                {
                    foreach (var singlecategory in cat.items)
                    {
                        o_category = o_category+singlecategory.name + ",";
                    }

                }
            }
            if (o_searchrecipe.text.Length == 0)
            {
                o_search = null;
            }
            else
            {
                o_search = o_searchrecipe.text;
            }
            if (o_searchrecipe.ingredients.Count > 0)
            {

                o_ingredients = o_ingredients.Remove(o_ingredients.Length - 1);
            }
            else
            {
                o_ingredients = null;
            }
            if (o_searchrecipe.category.Count > 0)
            {
                o_category = o_category.Remove(o_category.Length - 1);
            }
            else
            {
                o_category = null;
            }
            List<RecipeDetail> _recipeList = _recipeRepo.GetRecipeSearchList(o_search, o_ingredients, o_category);

            return new OkObjectResult(_recipeList);
        }
    }
}
