using Bll.Repository.Implementations;
using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository
{
    public class UnitOfWork : IDisposable
    {

        private FloorballEntities ctx;
        private IEventRepository eventRepository;
        private ILeagueRepository leagueRepository;
        private IMatchRepository matchRepository;
        private IPlayerRepository playerRepository;
        private IRefereeRepository refereeRepository;
        private ITeamRepository teamRepository;

        [Inject]
        public IEventRepository EventRepository
        {
            get { return eventRepository; }
            set { eventRepository = value; }
        }


        [Inject]
        public ILeagueRepository LeagueRepository
        {
            get { return leagueRepository; }
            set { leagueRepository = value; }
        }

        [Inject]
        public IMatchRepository MatchRepository
        {
            get { return matchRepository; }
            set { matchRepository = value; }
        }

        [Inject]
        public IPlayerRepository PlayerRepository
        {
            get { return playerRepository; }
            set { playerRepository = value; }
        }

        [Inject]
        public IRefereeRepository RefereeRepository
        {
            get { return refereeRepository; }
            set { refereeRepository = value; }
        }

        [Inject]
        public ITeamRepository TeamRepository
        {
            get { return teamRepository; }
            set { teamRepository = value; }
        }

        public UnitOfWork(FloorballEntities ctx)
        {
            this.ctx = ctx;
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
