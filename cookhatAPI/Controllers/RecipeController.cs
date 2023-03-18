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

        public async Task<ActionResult<bool>> AddRecipe([FromBody] AddRecipe recipe)
        {
           // var body = await new StreamReader(req.Body).ReadToEndAsync();
            AddRecipe request = recipe;
            bool _recipe = _recipeRepo.AddRecipe(request);
            return new OkObjectResult(_recipe);
        }
    }
}
