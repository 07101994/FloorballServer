using DAL.Ninject;
using DAL.Repository;
using DAL.Security;
using DAL.Util;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace FloorballServer.Providers
{
    public class FloorballAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        //[Inject]
        public IUnitOfWork UoW { get; set; }

        public FloorballAuthorizationServerProvider()
        {
            var kernel = new StandardKernel(new Bindings());

            UoW = kernel.Get<IUnitOfWork>();
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.Validated();
                context.SetError("invalid_clientId", "ClientId should be sent.");
            }

            Client client = await Task.Run(() => UoW.SecurityRepository.GetClient(context.ClientId));

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
            }

            if (string.IsNullOrWhiteSpace(clientSecret))
            {
                context.SetError("invalid_clientId", "Client secret should be sent.");
            }
            else
            {
                if (client.Secret != PasswordHasher.GetHash(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret is invalid.");
                }
            }

            if (!client.IsActive)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
            }

            context.OwinContext.Set("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin") ?? "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var user =  await Task.Run(()=> UoW.UserRepository.GetUser(context.UserName, context.Password));

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }


            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", user.Roles.First().RoleId));

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                },
                {
                    "userName", context.UserName
                }
            });

            var ticket = new AuthenticationTicket(identity, props);

            context.Validated(ticket);

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            context.Validated(new AuthenticationTicket(context.Ticket.Identity, context.Ticket.Properties));

            return Task.FromResult<object>(null);

        }

    }
}