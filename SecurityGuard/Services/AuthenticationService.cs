using System;
using System.Web.Security;
using SecurityGuard.Interfaces;
using SecurityGuard.ViewModels;

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

        public bool LogOn(LogOnViewModel model)
        {
            if (membershipService.ValidateUser(model.UserName, model.Password))
            {
                formsAuthenticationService.SetAuthCookie(model.UserName, model.RememberMe);
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
