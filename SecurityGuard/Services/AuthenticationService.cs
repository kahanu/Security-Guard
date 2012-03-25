using System;
using SecurityGuard.Interfaces;

namespace SecurityGuard.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        #region ctors

        private readonly IMembershipService membershipService;
        private readonly IFormsAuthenticationService formsAuthenticationService;

        public AuthenticationService(IMembershipService membershipService, IFormsAuthenticationService formsAuthenticationService)
        {
            this.formsAuthenticationService = formsAuthenticationService;
            this.membershipService = membershipService;
        }

        #endregion

        #region IAuthenticationService Members

        public bool LogOn(string userName, string password, bool rememberMe)
        {
            if (membershipService.ValidateUser(userName, password))
            {
                formsAuthenticationService.SetAuthCookie(userName, rememberMe);
                return true;
            }

            return false;   
        }

        public void LogOff()
        {
            formsAuthenticationService.SignOut();
        }

        #endregion
    }
}
