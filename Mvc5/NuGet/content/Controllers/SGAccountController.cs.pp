using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using $rootnamespace$.Mailers;
using $rootnamespace$.Mailers.Models;
using SecurityGuard.Core;
using SecurityGuard.Interfaces;
using SecurityGuard.Services;
using SecurityGuard.ViewModels;
using viewModels = $rootnamespace$.Areas.SecurityGuard.ViewModels;

namespace $rootnamespace$.Controllers
{
    /// <summary>
    /// This class handles all the normal logon, logoff, 
    /// register, change password, and forgot password operations
    /// that occur in the public part of your web application.
    /// </summary>
    public class SGAccountController : BaseController
    {
                
        #region ctors

        private IMembershipService membershipService;
        private IAuthenticationService authenticationService;
        private IFormsAuthenticationService formsAuthenticationService;
        private IPasswordResetMailer _mailer = new PasswordResetMailer();

        public SGAccountController()
        {
            this.membershipService = new MembershipService(Membership.Provider);
            this.authenticationService = new AuthenticationService(membershipService, new FormsAuthenticationService());
            this.formsAuthenticationService = new FormsAuthenticationService();
        }

        #endregion

        #region Public Properties

        public IPasswordResetMailer Mailer
        {
            get { return _mailer; }
            set { _mailer = value; }
        }

        #endregion

        public virtual ActionResult Index()
        {
            return View();
        }

        #region LogOn Methods

        [HttpGet]
        public virtual ActionResult Login()
        {
            var viewModel = new LogOnViewModel()
            {
                EnablePasswordReset = membershipService.EnablePasswordReset
            };
            return ContextDependentView(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authenticationService.LogOn(model.UserName, model.Password, model.RememberMe))
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
                    if (user == null)
                    {
                        ModelState.AddModelError("", "This account does not exist. Please try again.");
                    }
                    else
                    {
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
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Login");
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken()]
        public virtual ActionResult LogOff()
        {
            authenticationService.LogOff();

            return RedirectToAction("Index", "Home");
        } 
        #endregion

        #region Register Methods

        public virtual ActionResult Register()
        {
            var model = new viewModels.RegisterViewModel()
            {
                RequireSecretQuestionAndAnswer = membershipService.RequiresQuestionAndAnswer
            };
            return ContextDependentView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public virtual ActionResult Register(viewModels.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                membershipService.CreateUser(model.UserName, model.Password, model.Email, model.SecretQuestion, model.SecretAnswer, true, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    formsAuthenticationService.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            return RedirectToAction("Register");
        }


        #endregion

        #region ChangePassword Methods
        
        /// <summary>
        /// This allows the logged on user to change his password.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public virtual ActionResult ChangePassword()
        {
            var viewModel = new ChangePasswordViewModel();

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public virtual ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("ChangePassword");
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public virtual ActionResult ChangePasswordSuccess()
        {
            return View();
        }


        #endregion

        #region Forgot Password Methods

        /// <summary>
        /// This allows the non-logged on user to have his password
        /// reset and emailed to him.
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            var viewModel = new ForgotPasswordViewModel()
            {
                RequireSecretQuestionAndAnswer = membershipService.RequiresQuestionAndAnswer
            };
            return View(viewModel);
        }

        /// <summary>
        /// This is the GET action to collect the answer and then continue.
        /// This is only hit if the web.config/system.web/membership provider is
        /// set with the attribute requiresQuestionAndAnswer="true".
        /// </summary>
        /// <param name="model">ForgotPasswordViewModel</param>
        /// <returns></returns>
        public virtual ActionResult EnterSecretAnswer(ForgotPasswordViewModel model)
        {
            return View(model);
        }

        /// <summary>
        /// Reset the password for the user and email it to him.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public virtual ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            string userName = membershipService.GetUserNameByEmail(model.Email);
            // Get the userName by the email address
            if (string.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("Email", "Email address does not exist. Please check your spelling and try again.");
                return RedirectToAction("ForgotPassword");
            }

            MembershipUser user = membershipService.GetUser(userName);
            if (user == null)
            {
                ModelState.AddModelError("", "The user does not exist.  Please check your entry and try again.");
                return RedirectToAction("ForgotPassword");
            }

            if (model.RequireSecretQuestionAndAnswer && model.Checked == false)
            {
                // Get the SecretQuestion
                model.SecretQuestion = user.PasswordQuestion;
                model.Checked = true;

                return RedirectToAction("EnterSecretAnswer", model);
            }            
            
            if (model.RequireSecretQuestionAndAnswer && model.Checked == true)
            {
                if (string.IsNullOrEmpty(model.SecretAnswer))
                {
                    ModelState.AddModelError("SecretAnswer", "The Secret Answer is required.");
                    return RedirectToAction("EnterSecretAnswer", model);
                }
            }



            // Now reset the password
            string newPassword = string.Empty;

            if (membershipService.RequiresQuestionAndAnswer)
            {
                try
                {
                    newPassword = user.ResetPassword(model.SecretAnswer);
                }
                catch (NullReferenceException)
                {
                    ModelState.AddModelError("PasswordAnswer", "The Secret Password is required.");
                    return RedirectToAction("EnterSecretAnswer", model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("PasswordAnswer", ex.Message);
                    return RedirectToAction("EnterSecretAnswer", model);
                }
            }
            else
            {
                newPassword = user.ResetPassword();
            }

            // Email the new pasword to the user
            try
            {
                SmtpSection smtp = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                // Set the MailerModel properties that will be passed to the MvcMailer object.
                // Feel free to modify the properties as you need.
                MailerModel m = new MailerModel();
                m.UserName = user.UserName;
                m.Password = newPassword;
                m.FromEmail = smtp.From;
                m.Subject = ConfigSettings.SecurityGuardEmailSubject;
                m.ToEmail = model.Email;

                Mailer.PasswordReset(m).Send();                
            }
            catch (Exception)
            {
            }

            return RedirectToAction("ForgotPasswordSuccess");
        }


        public ActionResult ForgotPasswordSuccess()
        {
            return View();
        }


        #endregion

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        #region Mailer Helpers

        /// <summary>
        /// **** OBSOLETE ***
        /// This method encapsulates the email function. 
        /// Now using MvcMailer instead.
        /// </summary>
        /// <param name="emailTo"></param>
        /// <param name="emailFrom"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtml"></param>
        //private void Mail(string emailTo, string emailFrom, string subject, string body, bool isHtml)
        //{
        //    Email email = new Email();
        //    email.ToList = emailTo;
        //    email.FromEmail = emailFrom;
        //    email.Subject = subject;
        //    email.MessageBody = body;
        //    email.isHTML = isHtml;
            
        //    email.SendEmail(email);

        //}

        /// <summary>
        /// This function builds the email message body from the ResetPassword.html file.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string BuildMessageBody(string userName, string password, string filePath)
        {
            string body = string.Empty;

            FileInfo fi = new FileInfo(Server.MapPath(filePath));
            string text = string.Empty;

            if (fi.Exists)
            {
                using (StreamReader sr = fi.OpenText())
                {
                    text = sr.ReadToEnd();
                }
                text = text.Replace("%UserName%", userName);
                text = text.Replace("%Password%", password);
            }
            body = text;

            return body;
        }

        #endregion
        
        #region Json Methods

        [HttpPost]
        public JsonResult JsonLogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authenticationService.LogOn(model.UserName, model.Password, model.RememberMe))
                {
                    return Json(new { success = true, redirect = returnUrl });
                }
                else
                {
                    MembershipUser user = membershipService.GetUser(model.UserName);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "This account does not exist. Please try again.");
                    }
                    else
                    {
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
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        [HttpPost]
        public JsonResult JsonRegister(viewModels.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                membershipService.CreateUser(model.UserName, model.Password, model.Email, model.SecretQuestion, model.SecretAnswer, true, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    formsAuthenticationService.SetAuthCookie(model.UserName, false);

                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            return Json(new { errors = GetErrorsFromModelState() });
        }

        #endregion

        #region Private Helpers

        private ActionResult ContextDependentView(object model)
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");

            if (actionName.ToLower().Contains("login"))
                model = (LogOnViewModel)model;
            else
                model = (viewModels.RegisterViewModel)model;

            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView(model);
            }
            else
            {
                ViewBag.FormAction = actionName;
                return View(model);
            }
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #endregion
        
    }
}
