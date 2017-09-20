using Bll.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Bll.Repository.Implementations
{
    class UserRepository : Repository, IUserRepository
    {

        private UserManager<IdentityUser> userManager;

        public override FloorballEntities Ctx
        {
            get
            {
                return base.Ctx;
            }
            set
            {
                base.Ctx = value;
                userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(base.Ctx));
            }
        }

        public IdentityUser GetUser(string userName, string password)
        {
            return userManager.Find(userName, password);
        }

        public IdentityResult CreateUser(string userName, string password, string userRole)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userName
            };
            
            return userManager.Create(user, password); 
        }
    }
}
