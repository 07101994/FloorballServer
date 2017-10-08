using Bll.Context;
using Bll.Seed;
using DAL.Context;
using DAL.DBInitializer;
using DAL.Model;
using DAL.Repository;
using DAL.Repository.Implementations;
using DAL.Repository.Interfaces;
using DAL.Seed;
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
        private FloorballBaseCtx Context { get; set; }

        public override void Load()
        {
            //Bind remote, local or demo context and seeders
            BindLocal();

            //Bind repositories
            Bind<ILeagueRepository>().To<LeagueRepository>();
            Bind<ITeamRepository>().To<TeamRepository>();
            Bind<IMatchRepository>().To<MatchRepository>();
            Bind<IPlayerRepository>().To<PlayerRepository>();
            Bind<IRefereeRepository>().To<RefereeRepository>();
            Bind<IStadiumRepository>().To<StadiumRepository>();
            Bind<IStatisticRepository>().To<StatisticRepository>();
            Bind<IEventRepository>().To<EventRepository>();
            Bind<IEventMessageRepository>().To<EventMessageRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<IFloorballRepository>().To<FlorballRepository>();
            Bind<ISecurityRepository>().To<SecurityRepository>();                                 

            //Bind UoW
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("Ctx", Context);

        }


        public override void Dispose(bool disposing)
        {
            if (Context != null)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }

        private void BindLocal()
        {
            //Bind dbcontext
            Bind<FloorballBaseCtx>().To<LocalCtx>();
            //Bind db initializer
            Bind<IDatabaseInitializer<LocalCtx>>().To<LocalDBInitializer<LocalCtx>.DropCreateDatabaseIfModelChangesInitializer>();
            //Bind seeder
            Bind<ISeeder>().To<LocalSeeder>();
        }

        private void BindRemote()
        {
            //Bind dbcontext
            Bind<FloorballBaseCtx>().To<RemoteCtx>();
            //Bind db initializer
            Bind<IDatabaseInitializer<RemoteCtx>>().To<RemoteDBInitializer<RemoteCtx>.DropCreateDatabaseIfModelChangesInitializer>();
            //Bind seeder
            Bind<ISeeder>().To<RemoteSeeder>();
        }

        private void BindDemo()
        {
            //Bind dbcontext
            Bind<FloorballBaseCtx>().To<DemoCtx>();
            //Bind db initializer
            Bind<IDatabaseInitializer<DemoCtx>>().To<DemoDBInitializer<DemoCtx>.DropCreateDatabaseIfModelChangesInitializer>();
            //Bind seeder
            Bind<ISeeder>().To<DemoSeeder>();
        }

    }
}
