using DAL.Model;
using DAL.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Seed
{
    public class BaseSeeder
    {
        private List<string> tables = new List<string>{ "Events", "Statistics","Matches", "Teams","Players", "Leagues", "Referees","Stadiums","Updates", "EventMessages" };
        private List<string> tablesWithoutPrimaryKey = new List<string> { "AspNetRoles","AspNetUSerClaims","AspNetUserLogins","AspNetUserRoles","AspNetUsers","Clients","PlayerMatch","RefereeMatch","PlayerTeam","RefreshTokens"  };

        static readonly string SuperAdminUserName = "Tomi";
        static readonly List<Role> Roles = new List<Role> { Role.SuperAdmin, Role.Admin, Role.User, Role.Fan };

        protected void Delete(FloorballBaseCtx ctx)
        {
            foreach (var table in tables)
            {
                ctx.Database.ExecuteSqlCommand("DELETE FROM [dbo].[" + table + "]");
                ctx.Database.ExecuteSqlCommand("DBCC CHECKIDENT('[dbo].["+table+"]', RESEED, 0)");
            }

            foreach (var table in tablesWithoutPrimaryKey)
            {
                ctx.Database.ExecuteSqlCommand("DELETE FROM " + table);
            }

        }

        protected void Init(FloorballBaseCtx ctx)
        {

            AddRoles(ctx);
            AddSuperAdmin(ctx);
            AddEventMessages(ctx);

            AddClients(ctx);

            ctx.SaveChanges();
        }

        private void AddClients(FloorballBaseCtx ctx)
        {

            //var ManagerSecret = Util.PasswordHasher.GetHash("ManagerApp");
            //var ConsumerSecret = Util.PasswordHasher.GetHash("ConsumerApp");

            ctx.Clients.Add(new Client { Id = "ManagerApp", AllowedOrigin = "*", ApplicationType = ApplicationType.Manager, IsActive = true, Name = "Manager Application", RefreshTokenLifeTime = 10, Secret = "gMGbgbwXK+ntUR+0kCfnIvmuMAZsu1+a3Eod8HN05kI=" });
            ctx.Clients.Add(new Client { Id = "ConsumerApp", AllowedOrigin = "*", ApplicationType = ApplicationType.Consumer, IsActive = true, Name = "Consumer Application", RefreshTokenLifeTime = 10, Secret = "t6wM7qs9XUcXCCsgEy18NcCQgiZ7NmgB1o/4p9+Mcuo=" });

        }

        protected ICollection<T> AddEntitiesById<T>(DbSet<T> entites, List<int> ids) where T : class
        {
            var list = new List<T>();

            foreach (var id in ids)
            {
                list.Add(entites.Find(id));
            }

            return list;
        }

        private void AddEventMessages(FloorballBaseCtx ctx)
        {
            ctx.EventMessages.Add(new EventMessage {Id = 1, Code = 201, Message = "Szabálytalan ütés" });
            ctx.EventMessages.Add(new EventMessage {Id = 2, Code = 202, Message = "Ellenfél ütőjének le-/visszafogása" });
            ctx.EventMessages.Add(new EventMessage {Id = 3, Code = 203, Message = "Ellenfél ütőjének fel-/megemelése" });
            ctx.EventMessages.Add(new EventMessage {Id = 4, Code = 204, Message = "Szabálytalan rugás (ellenfél vagy ellenfél ütője)" });
            ctx.EventMessages.Add(new EventMessage {Id = 5, Code = 205, Message = "Magas láb (derék felett)" });
            ctx.EventMessages.Add(new EventMessage {Id = 6, Code = 206, Message = "Magasütő (Ütője bármely részével derék felett)" });
            ctx.EventMessages.Add(new EventMessage {Id = 7, Code = 207, Message = "Szabálytalan lökés" });
            ctx.EventMessages.Add(new EventMessage {Id = 8, Code = 208, Message = "Durvaság (lerántás, gáncsolás)" });
            ctx.EventMessages.Add(new EventMessage {Id = 9, Code = 209, Message = "Fogás" });
            ctx.EventMessages.Add(new EventMessage {Id = 10, Code = 210, Message = "Akadályozás, feltartás" });
            ctx.EventMessages.Add(new EventMessage {Id = 11, Code = 211, Message = "Szabálytalan távolság" });
            ctx.EventMessages.Add(new EventMessage {Id = 12, Code = 212, Message = "Fekvőjáték" });
            ctx.EventMessages.Add(new EventMessage {Id = 13, Code = 213, Message = "Kezezés" });
            ctx.EventMessages.Add(new EventMessage {Id = 14, Code = 214, Message = "Fejelés" });
            ctx.EventMessages.Add(new EventMessage {Id = 15, Code = 215, Message = "Szabálytalan csere" });
            ctx.EventMessages.Add(new EventMessage {Id = 16, Code = 216, Message = "Túl sok játékos a pályán" });
            ctx.EventMessages.Add(new EventMessage {Id = 17, Code = 217, Message = "Ismétlődő szabálytalanságok" });
            ctx.EventMessages.Add(new EventMessage {Id = 18, Code = 218, Message = "Játék késleltetése" });
            ctx.EventMessages.Add(new EventMessage {Id = 19, Code = 219, Message = "Reklamálás" });
            ctx.EventMessages.Add(new EventMessage {Id = 20, Code = 220, Message = "Engedély nálküli játéktérre lépés" });
            ctx.EventMessages.Add(new EventMessage {Id = 21, Code = 221, Message = "Szabálytalan felszerelés (személyes felsz., ruházat, sisak" });
            ctx.EventMessages.Add(new EventMessage {Id = 22, Code = 222, Message = "Csapatkapitány ütő mérését kéri, de szabályos" });
            ctx.EventMessages.Add(new EventMessage {Id = 23, Code = 223, Message = "Szabálytalan számozás" });
            ctx.EventMessages.Add(new EventMessage {Id = 24, Code = 224, Message = "Ütő nélküli játék" });
            ctx.EventMessages.Add(new EventMessage {Id = 25, Code = 225, Message = "Törött ütő eltávolításának hiánya a pályáról" });
            ctx.EventMessages.Add(new EventMessage {Id = 26, Code = 501, Message = "Erőszakos ütés" });
            ctx.EventMessages.Add(new EventMessage {Id = 27, Code = 502, Message = "Veszélyes játék" });
            ctx.EventMessages.Add(new EventMessage {Id = 28, Code = 503, Message = "Akasztás" });
            ctx.EventMessages.Add(new EventMessage {Id = 29, Code = 504, Message = "Játékos eldobja az ütőjét vagy felszerelését, eldobja magát" });
            ctx.EventMessages.Add(new EventMessage {Id = 30, Code = 504, Message = "Játékos ellenfelét veszályes módon támadja" });
            ctx.EventMessages.Add(new EventMessage {Id = 31, Code = 504, Message = "Játékos ellenfelét palánknak vagy a kapunak löki" });
            ctx.EventMessages.Add(new EventMessage {Id = 32, Code = 505, Message = "Játékos sorozatosan 2 perces kiállításhoz vezető szabálytalanságokat követ el" });
            ctx.EventMessages.Add(new EventMessage {Id = 33, Code = 301, Message = "Végleges kiállítás I. - technikai" });
            ctx.EventMessages.Add(new EventMessage {Id = 34, Code = 301, Message = "Végleges kiállítás I. - játékos mádosszor kap 2+10-et" });
            ctx.EventMessages.Add(new EventMessage {Id = 35, Code = 301, Message = "Végleges kiállítás I. - ütő szándékos eltörése" });
            ctx.EventMessages.Add(new EventMessage {Id = 36, Code = 301, Message = "Végleges kiállítás I. - súlyosan durva szabálytalanság" });
            ctx.EventMessages.Add(new EventMessage {Id = 37, Code = 302, Message = "Végleges kiállítás II. - dulakodás" });
            ctx.EventMessages.Add(new EventMessage {Id = 38, Code = 302, Message = "Végleges kiállítás II. - játék szabotálása" });
            ctx.EventMessages.Add(new EventMessage {Id = 39, Code = 302, Message = "Végleges kiállítás II. - csapatvezetés másodszor kap 2+10-et" });
            ctx.EventMessages.Add(new EventMessage {Id = 40, Code = 302, Message = "Végleges kiállítás II. - játékos másodszor kap 5 percet" });
            ctx.EventMessages.Add(new EventMessage {Id = 41, Code = 302, Message = "Végleges kiállítás II. - megerősített vagy mérés előtt javított ütő" });
            ctx.EventMessages.Add(new EventMessage {Id = 42, Code = 303, Message = "Végleges kiállítás III. - verekedés" });
            ctx.EventMessages.Add(new EventMessage {Id = 43, Code = 303, Message = "Végleges kiállítás III. - brutális szabálytalanság" });
            ctx.EventMessages.Add(new EventMessage {Id = 44, Code = 303, Message = "Végleges kiállítás III. - gyalázkodó beszéd" });
            ctx.EventMessages.Add(new EventMessage {Id = 45, Code = 401, Message = "Időkérés" });
            ctx.EventMessages.Add(new EventMessage {Id = 46, Code = 402, Message = "Büntetőlövés" });
            ctx.EventMessages.Add(new EventMessage {Id = 47, Code = 601, Message = "Emberelőnyös" });
            ctx.EventMessages.Add(new EventMessage {Id = 48, Code = 602, Message = "Emberhátrányos" });
            ctx.EventMessages.Add(new EventMessage {Id = 49, Code = 603, Message = "Üreskapus" });
            ctx.EventMessages.Add(new EventMessage {Id = 50, Code = 604, Message = "Büntetőből szerzett" });
            ctx.EventMessages.Add(new EventMessage {Id = 51, Code = 606, Message = "Normál" });
            ctx.EventMessages.Add(new EventMessage {Code = -1, Message = "Gólpassz" });

        }

        private void AddSuperAdmin(FloorballBaseCtx ctx)
        {
            //var pw = PasswordHasher.HashPassword("Initial1");

            ctx.Users.Add(new IdentityUser { UserName = SuperAdminUserName, PasswordHash = "AA/MTtdH588Of/vRRnB9/gC73Gt/XTQx+5TVtm85Tf2XDjyXrP/fuIt41uoPm55yKw==" });

            ctx.SaveChanges();

            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(ctx));

            userManager.AddToRole(userManager.FindByName(SuperAdminUserName).Id, Role.SuperAdmin.ToString());

        }

        private void AddRoles(FloorballBaseCtx ctx)
        {
            foreach (var role in Roles)
            {
                ctx.Roles.Add(new IdentityRole { Name = role.ToString() });
            }

            ctx.SaveChanges();
        }
    }
}
