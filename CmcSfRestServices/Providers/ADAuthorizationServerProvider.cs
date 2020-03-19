using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.Repositories;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Shared.Commands;

namespace CmcSfRestServices.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class ADAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly SecurityRepository _securityRepository;

        /// <summary>
        /// Constructor with SecurityRepository dependency
        /// </summary>
        /// <param name="securityRepository"></param>
        public ADAuthorizationServerProvider(SecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        ///// <summary>
        ///// Id that is returned in all http responses and is logged in Entlogger database
        ///// </summary>
        //public Guid CorrelationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() =>
            {
                context.Validated();
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //If you need to debug this code and breakpoints aren't being hit, Use Debug Attach to process w3wp.exe (attach to appropriate appPool)

            //// Alternate way to get user information from AD 
            ////  Connection information
            //    var connectionUsername = @"main\a-XXXX"; //@"main\ErmsServiceDev";
            //    var connectionPassword = "ppppppp"; //"Case27false?";
            //                                           // Get groups for this user
            //    var username = context.UserName;

            ////  Create context to connect to AD
            //    PrincipalContext context = new PrincipalContext(ContextType.Domain, "hq-dc1.main.cmcenergy.com", connectionUsername, connectionPassword);
            //    // Get User
            //    UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username);


            try
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                var userPrincipal = await IsCredentialsValid(context.UserName, context.Password);
                if (userPrincipal == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                var roles = userPrincipal.GetGroups().Where(u => u.Name.ToUpper().StartsWith("SALESFORCE")).ToList();
                //var roles = userPrincipal.GetAuthorizationGroups().Where(u => u.Name.ToUpper().StartsWith("ERMS")).ToList();

                //When setting up AD Roles for a Salesforce User, the user must have at least one "Environment Role"  (SalesforceUsersDev, SalesforceUsersTest,SalesforceUsersProd)
                //Check to see if the user has permissions for current environment.  

                bool isUserAllowedAccessToThisEnvironment = false;
                foreach (var role in roles)
                {
                    if (role.Name.ToUpper() == "SALESFORCEENV" + ConfigurationManager.AppSettings["DeployType"].ToUpper())
                    {
                        isUserAllowedAccessToThisEnvironment = true;
                    }
                    else
                        identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                }

                if (!isUserAllowedAccessToThisEnvironment)
                {
                    context.SetError("User is not permitted access to this environment");
                }

                else
                {
                    //Add special permission for all users to sign in

                    identity.AddClaim(new Claim("SFAdUserName", context.UserName));

                    var authTicket = new AuthenticationTicket(identity, null);  //new AuthenticationTicket(identity, tokenProps);
                    context.Validated(authTicket);  //authTicket causes .ussed and .Expires tokens to be returned in addition to user defined tokens

                    //context.Validated(identity); 
                }
            }
            catch (Exception ex)
            {
                context.SetError(ex.ToString());
                throw;
            }
        }


        /// <summary>
        /// Need to override this method to send extended properties of the token to client
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //public async Task<bool> IsCredentialsValid(string userName, string password)
        public async Task<UserPrincipal> IsCredentialsValid(string userName, string password)
        {
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                PrincipalContext pc = GetPrincipalContext(context);
                //                   return pc.ValidateCredentials(userName, password);
                if (pc.ValidateCredentials(userName, password))
                {
                    return await Task.Run(() =>
                    UserPrincipal.FindByIdentity(pc, IdentityType.Name, userName));
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>

        public async Task<UserPrincipal> GetUser(string sUserName)
        {
            return await Task.Run(() =>
            {
                using (var context = new PrincipalContext(ContextType.Domain))
                {
                    var pc = GetPrincipalContext(context);
                    return UserPrincipal.FindByIdentity(pc, IdentityType.Name, sUserName);
                }
            });
        }


        private static PrincipalContext GetPrincipalContext(PrincipalContext context)
        {
            var dcOverride = ConfigurationManager.AppSettings["OverrideDomainControllerWith"];
            context = string.IsNullOrWhiteSpace(dcOverride) ? new PrincipalContext(ContextType.Domain, context.ConnectedServer) : new PrincipalContext(ContextType.Domain, dcOverride);
            return context;
        }


    }
}
