using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DAL.Model;

namespace DAL.Repository.Implementations
{
    class UserRepository : FlorballRepository, IUserRepository
    {

        private UserManager<IdentityUser> userManager;

        public override FloorballBaseCtx Ctx
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

            var result = userManager.Create(user, password);

            userManager.AddToRole(user.Id, userRole);
            return userManager.AddToRole(user.Id, userRole);
        }
    }
}
