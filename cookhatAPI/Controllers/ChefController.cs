using cookhatAPI.Interface;
using cookhatAPI.Modal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefController : Controller
    {
        private IChef _chefRepo;
        public ChefController(IChef chefRepo)
        {
            _chefRepo = chefRepo;
        }
        [HttpPost]
        [Route("CreateChef")]

        public ActionResult<bool> CreateChefAccount([FromBody] Credentials recipe)
        {
            Credentials request = recipe;
            bool _recipe = _chefRepo.CreateChef(request);
            return new OkObjectResult(_recipe);
        }

        [HttpGet]
        [Route("GetChefDetail")]

        public ActionResult<ChefDetail> GetRecipeDetail(string chefid)
        {
            ChefDetail _recipe = _chefRepo.GetChefDetail(chefid);
            return new OkObjectResult(_recipe);
        }

        [HttpGet]
        [Route("GetChefList")]
        public ActionResult<List<ChefDetail>> GetRecipeList(string chefid)
        {
            string infid = chefid;
            if (infid == null)
            {
                infid = "";
            }
            List<ChefDetail> _recipe = _chefRepo.GetChefList(infid);
            return new OkObjectResult(_recipe);
        }

        [HttpGet]
        [Route("GetChefSearchList")]
        public ActionResult<List<ChefDetail>> GetChefSearch(string searchterm)
        {
            string osearchterm = searchterm;
            if (osearchterm == null || osearchterm == "undefined")
            {
                osearchterm = "";
            }
            List<ChefDetail> _recipe = _chefRepo.GetChefSearchList(osearchterm);
            return new OkObjectResult(_recipe);
        }
    }
}
