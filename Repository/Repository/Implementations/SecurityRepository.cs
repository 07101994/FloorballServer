using DAL.Repository.Interfaces;
using DAL.Security;
using DAL.Util;
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

        public Client GetClient(ApplicationType appType)
        {
            return Ctx.Clients.Where(c => c.ApplicationType == appType).SingleOrDefault();
        }

        public RefreshToken GetRefreshToken(string tokenId)
        {
            return Ctx.RefreshTokens.Find(tokenId);
        }

        public RefreshToken GetRefreshToken(string subject, string clientId)
        {
            return Ctx.RefreshTokens.Where(t => t.Subject == subject && t.ClientId == clientId).SingleOrDefault();
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

            try
            {
                Ctx.RefreshTokens.Remove(token);
            }
            catch (Exception)
            {
            }

            return Ctx.SaveChanges() > 0;
        }
    }
}
