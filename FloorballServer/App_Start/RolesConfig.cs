using DAL.Ninject;
using DAL.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FloorballServer.App_Start
{
    public class RolesConfig
    {

        //[Inject]
        static public IUnitOfWork UoW { get; set; }
        
        private static RolesConfig instance;

        public static RolesConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RolesConfig();
                }

                return instance;
            }
        }

        protected RolesConfig()
        {
            var kernel = new StandardKernel(new Bindings());
            
            UoW = kernel.Get<IUnitOfWork>();

        }

        public void AddRoles()
        {
            UoW.UserRepository.CreateUser("Tomi", "SDSDSDSD","");

            AddRole("SuperAdmin");
            AddRole("Admin");
            AddRole("User");
            AddRole("Fan");
        }

        private void AddRole(string roleName)
        {
            try
            {
                UoW.RoleRepository.CreateRole(roleName);
            }
            catch (Exception)
            {
            }
        }

    }
}