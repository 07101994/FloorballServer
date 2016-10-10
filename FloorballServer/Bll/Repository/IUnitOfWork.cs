using Bll.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository
{
    public interface IUnitOfWork : IDisposable
    {

        IEventRepository EventRepository 
        {
            get;
            set;
        }


        ILeagueRepository LeagueRepository
        {
            get;
            set;
        }

        IMatchRepository MatchRepository
        {
            get;
            set;
        }

        IPlayerRepository PlayerRepository
        {
            get;
            set;
        }

        IRefereeRepository RefereeRepository
        {
            get;
            set;
        }

        ITeamRepository TeamRepository
        {
            get;
            set;
        }

        IEventMessageRepository EventMessageRepository
        {
            get;
            set;
        }

        IRepository Repository
        {
            get;
            set;
        }

        IStatisticRepository StatisticRepository
        {
            get;
            set;
        }

        IStadiumRepository StadiumRepository
        {
            get;
            set;
        }

    }
}
