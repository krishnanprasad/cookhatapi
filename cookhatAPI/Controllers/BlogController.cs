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
    public class BlogController : Controller
    {
        private IBlog _blogRepo;


        public BlogController(IBlog blogRepo)
        {
            _blogRepo = blogRepo;
        }
        [HttpGet]
        [Route("GetBlogDetail")]

        public ActionResult<Blogs> GetBlogDetail(string blogid)
        {
            string recipeid = blogid;
            Blogs _blog = _blogRepo.GetBlogDetail(blogid);
            return new OkObjectResult(_blog);
        }

        [HttpGet]
        [Route("GetBlogList")]
        public ActionResult<Blogs> GetBlogList()
        {
            List<Blogs> _blogList = _blogRepo.GetRecommendedBlogList();
            return new OkObjectResult(_blogList);
        }

        [HttpGet]
        [Route("GetTrendingBlogList")]
        public ActionResult<Blogs> GetTrendingBlogList()
        {
            List<Blogs> _blogList = _blogRepo.GetTrendingBlogList();
            return new OkObjectResult(_blogList);
        }
    }
}
