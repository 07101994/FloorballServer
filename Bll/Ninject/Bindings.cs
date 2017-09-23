using Bll.Context;
using Bll.DBInitializer;
using Bll.Seed;
using DAL.Model;
using DAL.Repository;
using DAL.Repository.Implementations;
using DAL.Repository.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Ninject
{
    public class Bindings : NinjectModule
    {

        private DbContext Context { get; set; }

        public override void Load()
        {
            //Bind dbcontext
            Bind<DbContext>().To<LocalCtx>();
            //Bind db initializer
            Bind<IDatabaseInitializer<FloorballBaseCtx>>().To<DropCreateDatabaseAlwaysInitializer>();
            //Bind seeder
            Bind<ISeeder>().To<LocalSeeder>();

            //Bind repositories
            Bind<ILeagueRepository>().To<LeagueRepository>().WithConstructorArgument("Ctx",Context);
            Bind<ITeamRepository>().To<TeamRepository>().WithConstructorArgument("Ctx", Context);
            Bind<IMatchRepository>().To<MatchRepository>().WithConstructorArgument("Ctx", Context);
            Bind<IPlayerRepository>().To<PlayerRepository>().WithConstructorArgument("Ctx", Context);
            Bind<IRefereeRepository>().To<RefereeRepository>().WithConstructorArgument("Ctx", Context);
            Bind<IStadiumRepository>().To<StadiumRepository>().WithConstructorArgument("Ctx", Context);
            Bind<IStatisticRepository>().To<StatisticRepository>().WithConstructorArgument("Ctx", Context);
            Bind<IEventRepository>().To<EventRepository>().WithConstructorArgument("Ctx", Context);
            Bind<IEventMessageRepository>().To<EventMessageRepository>().WithConstructorArgument("Ctx", Context);
            Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument("Ctx", Context);
            Bind<IRoleRepository>().To<RoleRepository>().WithConstructorArgument("Ctx", Context);
            Bind<IRepository>().To<Repository.Implementations.Repository>().WithConstructorArgument("Ctx", Context);

            //Bind UoW
            Bind<IUnitOfWork>().To<UnitOfWork>();

        }


        public override void Dispose(bool disposing)
        {
            if (Context != null)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
