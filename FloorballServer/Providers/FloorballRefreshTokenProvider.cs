using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Ninject;
using DAL.Repository;
using DAL.Ninject;
using DAL.Security;
using DAL.Util;

namespace FloorballServer.Providers
{
    public class FloorballRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public IUnitOfWork UoW { get; set; }

        public FloorballRefreshTokenProvider()
        {
            var kernel = new StandardKernel(new Bindings());

            UoW = kernel.Get<IUnitOfWork>();
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = new RefreshToken()
            {
                Id = PasswordHasher.GetHash(refreshTokenId),
                ClientId = clientid,
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            if (UoW.SecurityRepository.AddRefreshToken(token))
            {
                context.SetToken(refreshTokenId);
            }

        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            await Task.Run(() => Create(context));
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = PasswordHasher.GetHash(context.Token);

            var refreshToken = UoW.SecurityRepository.GetRefreshToken(hashedTokenId);

            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                var result = UoW.SecurityRepository.RemoveRefreshToken(hashedTokenId);
            }

        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            await Task.Run(() => Receive(context));
        }
    }
}