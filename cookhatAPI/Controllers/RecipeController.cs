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
        
        public ActionResult<RecipeDetail> GetRecipeDetail (string id)
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

        public ActionResult<RecipeChef> GetRecipeList(string? inf_id)
        {
            string infid= inf_id;
            if(infid==null)
            {
                infid = "";
            }
            List<RecipeChef> _recipeList = _recipeRepo.GetRecipeList(infid);
            return new OkObjectResult(_recipeList);
        }
        [HttpGet]
        [Route("GetSearchRecipeList")]

        public ActionResult<RecipeChef> GetRecipeSearchList(string? searchRecipe)
        {
            string o_searchrecipe = searchRecipe;
            if (o_searchrecipe == null)
            {
                o_searchrecipe = "";
            }
            List<RecipeDetail> _recipeList = _recipeRepo.GetRecipeSearchList(o_searchrecipe);
            return new OkObjectResult(_recipeList);
        }
    }
}
