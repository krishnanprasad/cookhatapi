using cookhatAPI.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Interface
{
    public interface IChef
    {
        bool CreateChef(Credentials chefcredentials);
        ChefDetail GetChefDetail(string chefid);
    }
}
