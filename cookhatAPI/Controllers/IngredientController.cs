using cookhatAPI.Interface;
using cookhatAPI.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private IIngredient _ingredientRepo;
        public IngredientController(IIngredient ingredientRepo)
        {
            _ingredientRepo = ingredientRepo;
        }
        [HttpGet]
        [Route("GetRecommendedIngredientList")]
        public ActionResult<Ingredient> GetRecommendedIngredientList(string? inf_id)
        {
           
            List<Ingredient> _ingredientList = _ingredientRepo.GetRecommendedIngredientList();
            return new OkObjectResult(_ingredientList);
        }
        [HttpGet]
        [Route("GetIngredientListSearch")]
        public ActionResult<Ingredient> GetIngredientListSearch(string? searchterm)
        {
            List<Ingredient> _ingredientList = _ingredientRepo.GetIngredientListSearch(searchterm);
            return new OkObjectResult(_ingredientList);
        }
    }
}
