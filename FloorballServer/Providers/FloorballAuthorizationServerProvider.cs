using Bll.Repository;
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

        [Inject]
        public IUnitOfWork UoW { get; set; }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var user = UoW.UserRepository.GetUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", user.Roles.First().RoleId));
            

            context.Validated(identity);

        }

    }
}