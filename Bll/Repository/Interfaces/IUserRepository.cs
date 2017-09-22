using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IUserRepository : IDisposable
    {

        #region READ

        IdentityUser GetUser(string userName, string password);

        #endregion

        #region CREATE

        IdentityResult CreateUser(string userName, string password, string userRole);

        #endregion
    }
}
