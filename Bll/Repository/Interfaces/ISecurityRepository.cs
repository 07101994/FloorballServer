using DAL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface ISecurityRepository : IRepositoryBase
    {
        Client GetClient(string clientId);

        bool AddRefreshToken(RefreshToken token);

        bool RemoveRefreshToken(string tokenId);

        bool RemoveRefreshToken(RefreshToken token);

        RefreshToken GetRefreshToken(string tokenId);

        IEnumerable<RefreshToken> GetRefreshTokens();


    }
}
