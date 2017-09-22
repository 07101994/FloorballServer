using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Repository.Implementations
{
    public class RoleRepository : Repository, IRoleRepository
    {

        private RoleManager<IdentityRole> roleManager;

        public override FloorballEntities Ctx
        {
            get
            {
                return base.Ctx;
            }
            set
            {
                base.Ctx = value;
                roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(base.Ctx));
            }
        }


        public IdentityRole GetRole(string id)
        {
            return roleManager.FindById(id);
        }

        public IdentityResult CreateRole(string roleName)
        {
            IdentityRole role = new IdentityRole
            {
                Name = roleName
            };

            return roleManager.Create(role);
        }
    }
}
