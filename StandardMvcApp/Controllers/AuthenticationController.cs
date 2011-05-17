using System.Web.Mvc;
using System.Web.Security;
using SecurityGuard.Services;
using SecurityGuard.Interfaces;
using SecurityGuard.ViewModels;

namespace StandardMvcApp.Controllers
{
    public partial class AuthenticationController : Controller
    {
        #region ctors

        private IMembershipService membershipService;
        private IAuthenticationService authenticationService;
        
        public AuthenticationController(IMembershipService membershipService, IFormsAuthenticationService formsAuthenticationService)
        {
            this.authenticationService = new AuthenticationService(membershipService, formsAuthenticationService);
            this.membershipService = membershipService;
        }

        public AuthenticationController()
            : this(new MembershipService(Membership.Provider),
            new FormsAuthenticationService())
        {

        }

        #endregion


        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult LogOn()
        {
            return View(new LogOnViewModel());
        }

        [HttpPost]
        public virtual ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authenticationService.LogOn(model))
                {
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    MembershipUser user = membershipService.GetUser(model.UserName);
                    if (!user.IsApproved)
                    {
                        ModelState.AddModelError("", "Your account has not been approved yet.");
                    }
                    else if (user.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Your account is currently locked.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public virtual ActionResult LogOff()
        {
            authenticationService.LogOff();

            return RedirectToAction("Index", "Home");
        }

    }
}
