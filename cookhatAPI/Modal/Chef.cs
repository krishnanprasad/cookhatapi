using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Modal
{
    public class Chef
    {
        public string ChefID { get; set; }
        public string ChefName { get; set; }
        public string ProfileImageSource { get; set; }
        public int FollowerCount { get; set; }
    }

    public class ChefDetail
    {
        public string chefid { get; set; }
        public string chefname { get; set; }
        public string chefimgurl { get; set; }
        public string cheffavouritecuisine { get; set; }
        public string chefcaption { get; set; }
        public string cheflocation { get; set; }
        public int totalfollowers { get; set; }
        public int totalrecipes { get; set; }
        public int totalproducts { get; set; }

        public DateTime chefaccountcreateddate { get; set; }
    }
}
