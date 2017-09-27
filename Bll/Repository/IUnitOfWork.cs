using DAL.Model;
using DAL.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IUnitOfWork : IDisposable
    {

        FloorballBaseCtx Ctx { get; set; }

        IEventRepository EventRepository { get; set; }

        ILeagueRepository LeagueRepository { get; set; }

        IMatchRepository MatchRepository { get; set; }

        IPlayerRepository PlayerRepository { get; set;  }

        IRefereeRepository RefereeRepository { get; set;  }

        ITeamRepository TeamRepository { get; set; }

        IEventMessageRepository EventMessageRepository { get; set; }

        IFloorballRepository Repository { get; set; }

        IStatisticRepository StatisticRepository { get; set; }

        IStadiumRepository StadiumRepository { get; set; }

        IUserRepository UserRepository { get; set; }

        IRoleRepository RoleRepository { get; set; }

        ISecurityRepository SecurityRepository { get; set; }

    }
}
