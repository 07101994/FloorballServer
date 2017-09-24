using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IRoleRepository : IRepositoryBase
    {
        #region READ

        IdentityRole GetRole(string id);

        #endregion

        #region CREATE

        IdentityResult CreateRole(string roleName);

        #endregion
    }
}
