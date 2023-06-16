
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace framework_4._7._2_B2C_MVC_WebApp
{
    public partial class Startup
    {
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
        private static string instance = EnsureTrailingSlash(ConfigurationManager.AppSettings["ida:Instance"]);
        private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string authorizationPolicyId = ConfigurationManager.AppSettings["ida:AuthorizationPolicyId"];
        private static string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];

        // B2C Authority = https://yourtenant.b2clogin.com/yourtenant.onmicrosoft.com/susiPolicyId/v2.0
        private static string authority = $"{instance}{tenantId}/{authorizationPolicyId}/v2.0/";

        public void ConfigureAuth(IAppBuilder app)
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    Authority = authority,
                    RedirectUri = redirectUri,

                    
                    // ClientId to make sure we get an access_token
                    Scope = $"{OpenIdConnectScope.OpenIdProfile} {OpenIdConnectScope.OfflineAccess} {clientId}",

                    ResponseType = OpenIdConnectResponseType.Code,
                    RedeemCode = true,

                    TokenValidationParameters = new TokenValidationParameters 
                    {
                        NameClaimType = "name",
                        ValidateIssuer = true
                        // custom issuer validation here...
                        // IssuerValidator = (issuer, token, validationParameters) => 
                    },
                    
                    //Notifications = new OpenIdConnectAuthenticationNotifications
                    //{
                    //    SecurityTokenValidated = context =>
                    //    {
                    //        context.AuthenticationTicket.Identity.AddClaim(new Claim("id_token", context.ProtocolMessage.IdToken));
                    //        context.AuthenticationTicket.Identity.AddClaim(new Claim("access_token", context.ProtocolMessage.AccessToken));
                    //        return Task.CompletedTask;
                    //    },
                    //    RedirectToIdentityProvider = n =>
                    //    {
                    //        // If signing out, add the id_token_hint
                    //        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.Logout)
                    //        {
                    //            var idTokenClaim = n.OwinContext.Authentication.User.FindFirst("id_token");
                    //            if (idTokenClaim != null)
                    //            {
                    //                n.ProtocolMessage.IdTokenHint = idTokenClaim.Value;
                    //            }
                    //        }
                    //        return Task.CompletedTask;
                    //    }
                    //}
                });
        }

        private static string EnsureTrailingSlash(string value)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            if (!value.EndsWith("/", StringComparison.Ordinal))
            {
                return value + "/";
            }

            return value;
        }
    }
}
