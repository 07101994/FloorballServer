using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        [Inject]
        IEventRepository EventRepository { get; set; }

        [Inject]
        ILeagueRepository LeagueRepository { get; set; }

        [Inject]
        IMatchRepository MatchRepository { get; set; }

        [Inject]
        IPlayerRepository PlayerRepository { get; set;  }

        [Inject]
        IRefereeRepository RefereeRepository { get; set;  }

        [Inject]
        ITeamRepository TeamRepository { get; set; }

        [Inject]
        IEventMessageRepository EventMessageRepository { get; set; }

        [Inject]
        IRepository Repository { get; set; }

        [Inject]
        IStatisticRepository StatisticRepository { get; set; }

        [Inject]
        IStadiumRepository StadiumRepository { get; set; }

    }
}
