using cookhatAPI.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Interface
{
    public interface IBlog
    {
        List<Blogs> GetRecommendedBlogList();
        Blogs GetBlogDetail (string? blogid);
    }
}
