using Bll.Repository.Implementations;
using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private FloorballEntities ctx;

        private IEventRepository eventRepository;
        private ILeagueRepository leagueRepository;
        private IMatchRepository matchRepository;
        private IPlayerRepository playerRepository;
        private IRefereeRepository refereeRepository;
        private ITeamRepository teamRepository;
        private IEventMessageRepository eventMessageRepository;
        private IRepository repository;
        private IStatisticRepository statisticRepository;
        private IStadiumRepository stadiumRepository;

        private StandardKernel kernel;

        public IEventRepository EventRepository
        {
            get;
            set;
        }

        public ILeagueRepository LeagueRepository
        {
            get;
            set;
        }

        public IMatchRepository MatchRepository
        {
            get;
            set;
        }

        public IPlayerRepository PlayerRepository
        {
            get;
            set;
        }

        public IRefereeRepository RefereeRepository
        {
            get;
            set;
        }

        public ITeamRepository TeamRepository
        {
            get;
            set;
        }

        public IEventMessageRepository EventMessageRepository
        {
            get;
            set;
        }

        public IRepository Repository
        {
            get;
            set;
        }

        public IStatisticRepository StatisticRepository
        {
            get;
            set;
        }

        public IStadiumRepository StadiumRepository
        {
            get;
            set; 
        }

        //public IEventRepository EventRepository
        //{
        //    get
        //    {
        //        return eventRepository;
        //    }

        //    set
        //    {
        //        eventRepository = value;
        //    }
        //}

        //public ILeagueRepository LeagueRepository
        //{
        //    get
        //    {
        //        return leagueRepository;
        //    }

        //    set
        //    {
        //        leagueRepository = value;
        //    }
        //}

        //public IMatchRepository MatchRepository
        //{
        //    get
        //    {
        //        return matchRepository;
        //    }

        //    set
        //    {
        //        matchRepository = value;
        //    }
        //}

        //public IPlayerRepository PlayerRepository
        //{
        //    get
        //    {
        //        return playerRepository;
        //    }

        //    set
        //    {
        //        playerRepository = value;
        //    }
        //}

        //public IRefereeRepository RefereeRepository
        //{
        //    get
        //    {
        //        return refereeRepository;
        //    }

        //    set
        //    {
        //        refereeRepository = value;
        //    }
        //}

        //public ITeamRepository TeamRepository
        //{
        //    get
        //    {
        //        return teamRepository;
        //    }

        //    set
        //    {
        //        teamRepository = value;
        //    }
        //}

        //public IEventMessageRepository EventMessageRepository
        //{
        //    get
        //    {
        //        return eventMessageRepository;
        //    }

        //    set
        //    {
        //        eventMessageRepository = value;
        //    }
        //}

        //public IRepository Repository
        //{
        //    get
        //    {
        //        return repository;
        //    }

        //    set
        //    {
        //        repository = value;
        //    }
        //}

        //public IStatisticRepository StatisticRepository
        //{
        //    get
        //    {
        //        return statisticRepository;
        //    }

        //    set
        //    {
        //        statisticRepository = value;
        //    }
        //}

        //public IStadiumRepository StadiumRepository
        //{
        //    get
        //    {
        //        return stadiumRepository;
        //    }

        //    set
        //    {
        //        stadiumRepository = value;
        //    }
        //}

        public UnitOfWork(FloorballEntities ctx)
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
