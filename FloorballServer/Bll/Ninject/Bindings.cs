using Bll.Repository;
using Bll.Repository.Implementations;
using Bll.Repository.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Ninject
{
    public class Bindings : NinjectModule
    {

        private DbContext Context { get; set; }

        public override void Load()
        {
            //Bind dbcontext
            Bind<DbContext>().To<FloorballEntities>();

            //Bind repositories
            Bind<ILeagueRepository>().To<LeagueRepository>().WithConstructorArgument("ctx",Context);
            Bind<ITeamRepository>().To<TeamRepository>().WithConstructorArgument("ctx", Context);
            Bind<IMatchRepository>().To<MatchRepository>().WithConstructorArgument("ctx", Context);
            Bind<IPlayerRepository>().To<PlayerRepository>().WithConstructorArgument("ctx", Context);
            Bind<IRefereeRepository>().To<RefereeRepository>().WithConstructorArgument("ctx", Context);
            Bind<IStadiumRepository>().To<StadiumRepository>().WithConstructorArgument("ctx", Context);
            Bind<IStatisticRepository>().To<StatisticRepository>().WithConstructorArgument("ctx", Context);
            Bind<IEventRepository>().To<EventRepository>().WithConstructorArgument("ctx", Context);
            Bind<IEventMessageRepository>().To<EventMessageRepository>().WithConstructorArgument("ctx", Context);
            Bind<IRepository>().To<Repository.Implementations.Repository>().WithConstructorArgument("ctx", Context);

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
