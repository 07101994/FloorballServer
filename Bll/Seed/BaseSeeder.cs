using DAL.Model;
using DAL.Util;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Seed
{
    public class BaseSeeder
    {
        static readonly string SuperAdminUserName = "Tomi";
        static readonly List<string> Roles = new List<string> { "SuperAdmin", "Admin", "User", "Fan" };

        protected void Init(FloorballBaseCtx ctx)
        {

            foreach (var role in Roles)
            {
                ctx.Roles.Add(new IdentityRole { Name = role });
            }

            //var pw = PasswordHasher.HashPassword("Initial1");

            ctx.Users.Add(new IdentityUser { UserName = SuperAdminUserName, PasswordHash = "AA/MTtdH588Of/vRRnB9/gC73Gt/XTQx+5TVtm85Tf2XDjyXrP/fuIt41uoPm55yKw=="});

            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(ctx));

            userManager.AddToRole(userManager.FindByName(SuperAdminUserName).Id, "SuperAdmin");

            ctx.SaveChanges();
        }

    }
}
