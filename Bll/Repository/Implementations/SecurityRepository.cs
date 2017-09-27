using DAL.Repository.Interfaces;
using DAL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Implementations
{
    public class SecurityRepository : FlorballRepository, ISecurityRepository
    {
        public bool AddRefreshToken(RefreshToken token)
        {
            var existingToken = Ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = RemoveRefreshToken(token);
            }

            Ctx.RefreshTokens.Add(token);

            return Ctx.SaveChanges() > 0;
        }

        public Client GetClient(string clientId)
        {
            return Ctx.Clients.Find(clientId);
        }

        public RefreshToken GetRefreshToken(string tokenId)
        {
            return Ctx.RefreshTokens.Find(tokenId);
        }

        public IEnumerable<RefreshToken> GetRefreshTokens()
        {
            return Ctx.RefreshTokens;
        }

        public bool RemoveRefreshToken(string tokenId)
        {
            return RemoveRefreshToken(Ctx.RefreshTokens.Find(tokenId));
        }

        public bool RemoveRefreshToken(RefreshToken token)
        {
            Ctx.RefreshTokens.Remove(token);

            return Ctx.SaveChanges() > 0;
        }
    }
}
