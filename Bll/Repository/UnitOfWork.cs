using DAL.Model;
using DAL.Repository.Implementations;
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
        //private FloorballBaseCtx ctx;

        private StandardKernel kernel;

        public FloorballBaseCtx Ctx { get; set; }

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

        public UnitOfWork()
        {
            kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            Ctx = kernel.Get<FloorballBaseCtx>();

            TeamRepository = kernel.Get<ITeamRepository>();
            TeamRepository.Ctx = Ctx;

            EventMessageRepository = kernel.Get<IEventMessageRepository>();
            EventMessageRepository.Ctx = Ctx;

            EventRepository = kernel.Get<IEventRepository>();
            EventRepository.Ctx = Ctx;

            MatchRepository = kernel.Get<IMatchRepository>();
            MatchRepository.Ctx = Ctx;

            PlayerRepository = kernel.Get<IPlayerRepository>();
            PlayerRepository.Ctx = Ctx;

            StadiumRepository = kernel.Get<IStadiumRepository>();
            StadiumRepository.Ctx = Ctx;

            StatisticRepository = kernel.Get<IStatisticRepository>();
            StatisticRepository.Ctx = Ctx;

            LeagueRepository = kernel.Get<ILeagueRepository>();
            LeagueRepository.Ctx = Ctx;

            RefereeRepository = kernel.Get<IRefereeRepository>();
            RefereeRepository.Ctx = Ctx;

            UserRepository = kernel.Get<IUserRepository>();
            UserRepository.Ctx = Ctx;

            RoleRepository = kernel.Get<IRoleRepository>();
            RoleRepository.Ctx = Ctx;

            //Repository = kernel.Get<IRepository>();

        }


        public void Save()
        {
            Ctx.SaveChanges();
            
        }

        private bool disposed = false;

        

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Ctx.Dispose();
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
