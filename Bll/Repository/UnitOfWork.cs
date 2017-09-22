using DAL.Model;
using DAL.Repository.Implementations;
using DAL.Repository.Interfaces;
using DAL.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private FloorballCtx ctx;

        private StandardKernel kernel;

        public IEventRepository EventRepository { get; set; }

        public ILeagueRepository LeagueRepository { get; set; }

        public IMatchRepository MatchRepository { get; set; }

        public IPlayerRepository PlayerRepository { get; set; }

        public IRefereeRepository RefereeRepository { get; set; }

        public ITeamRepository TeamRepository { get; set; }

        public IEventMessageRepository EventMessageRepository { get; set; }

        public IRepository Repository { get; set; }

        public IStatisticRepository StatisticRepository { get; set; }

        public IStadiumRepository StadiumRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public IRoleRepository RoleRepository { get; set; }

        public UnitOfWork(FloorballCtx ctx)
        {
            this.ctx = ctx;
            kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            TeamRepository = kernel.Get<ITeamRepository>();
            EventMessageRepository = kernel.Get<IEventMessageRepository>();
            EventRepository = kernel.Get<IEventRepository>();
            MatchRepository = kernel.Get<IMatchRepository>();
            PlayerRepository = kernel.Get<IPlayerRepository>();
            StadiumRepository = kernel.Get<IStadiumRepository>();
            StatisticRepository = kernel.Get<IStatisticRepository>();
            LeagueRepository = kernel.Get<ILeagueRepository>();
            RefereeRepository = kernel.Get<IRefereeRepository>();
            UserRepository = kernel.Get<IUserRepository>();
            RoleRepository = kernel.Get<IRoleRepository>();
            Repository = kernel.Get<IRepository>();

        }


        public void Save()
        {
            ctx.SaveChanges();
            
        }

        private bool disposed = false;

        

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
