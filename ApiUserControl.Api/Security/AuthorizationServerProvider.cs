using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using ApiUserControl.Common.Resources;
using ApiUserControl.Domain.Contracts.Services;
using Microsoft.Owin.Security.OAuth;

namespace ApiUserControl.Api.Security
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private IUserService _userService;

        public AuthorizationServerProvider(IUserService userService)
        {
            _userService = userService;
        }
        
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
           
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                var user = _userService.Authenticate(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", Errors.InvalidCredentials);
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));

                GenericPrincipal principal = new GenericPrincipal(identity, null);
                Thread.CurrentPrincipal = principal;

                context.Validated(identity);
            }
            catch(Exception ex)
            {
                context.SetError("invalid_grant", Errors.InvalidCredentials);
            }
        }
    }
}