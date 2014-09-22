using System;
using System.Web.Mvc;
using System.Web.Security;
using SecurityGuard.Core.Extensions;
using SecurityGuard.Services;
using SecurityGuard.Core.Attributes;
using routeHelpers = SecurityGuard.Core.RouteHelpers;
using SecurityGuard.Interfaces;
using SecurityGuard.ViewModels;
using $rootnamespace$.Controllers;
using viewModels = $rootnamespace$.Areas.SecurityGuard.ViewModels;

namespace $rootnamespace$.Areas.SecurityGuard.Controllers
{
    [Authorize(Roles = "SecurityGuard")]
    public partial class MembershipController : BaseController
    {

        #region ctors

        private IMembershipService membershipService;
        private readonly IRoleService roleService;

        public MembershipController()
        {
            this.roleService = new RoleService(Roles.Provider);
            this.membershipService = new MembershipService(Membership.Provider);
        }

        #endregion

        #region Index Method
        public virtual ActionResult Index(string filterby, string searchterm)
        {
            ManageUsersViewModel viewModel = new ManageUsersViewModel();
            viewModel.Users = null;
            viewModel.FilterBy = filterby;
            viewModel.SearchTerm = searchterm;

            int totalRecords;
            int page = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["pagesize"]);
            if (pageSize == 0)
                pageSize = 25;

            viewModel.PageSize = pageSize;

            if (!string.IsNullOrEmpty(filterby))
            {
                if (filterby == "all")
                {
                    viewModel.PaginatedUserList = membershipService.GetAllUsers(page, pageSize, out totalRecords).ToPaginatedList(page, pageSize, totalRecords, searchterm, filterby);
                }
                else if (!string.IsNullOrEmpty(searchterm))
                {
                    string query = searchterm + "%";
                    if (filterby == "email")
                    {
                        viewModel.PaginatedUserList = membershipService.FindUsersByEmail(query, page, pageSize, out totalRecords).ToPaginatedList(page, pageSize, totalRecords, searchterm, filterby);
                    }
                    else if (filterby == "username")
                    {
                        viewModel.PaginatedUserList = membershipService.FindUsersByName(query, page, pageSize, out totalRecords).ToPaginatedList(page, pageSize, totalRecords, searchterm, filterby);
                    }
                }
            }

            return View(viewModel);
        }
        #endregion

        #region Create User Methods

        public virtual ActionResult CreateUser()
        {
            var model = new viewModels.RegisterViewModel()
            {
                RequireSecretQuestionAndAnswer = membershipService.RequiresQuestionAndAnswer
            };
            return View(model);
        }

        /// <summary>
        /// This method redirects to the GrantRolesToUser method.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult CreateUser(viewModels.RegisterViewModel model)
        {
            MembershipUser user;
            MembershipCreateStatus status;
            user = membershipService.CreateUser(model.UserName, model.Password, model.Email, model.SecretQuestion, model.SecretAnswer, model.Approve, out status);

            return routeHelpers.Actions.GrantRolesToUser(user.UserName);
        }

        /// <summary>
        /// An Ajax method to check if a username is unique.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckForUniqueUser(string userName)
        {
            MembershipUser user = membershipService.GetUser(userName);
            JsonResponse response = new JsonResponse();
            response.Exists = (user == null) ? false : true;

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Delete User Methods

        [HttpPost]
        [MultiButtonFormSubmit(ActionName = "UpdateDeleteCancel", SubmitButton = "DeleteUser")]
        public ActionResult DeleteUser(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                throw new ArgumentNullException("userName");
            }

            try
            {
                membershipService.DeleteUser(UserName);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "There was an error deleting this user. - " + ex.Message;
            }

            return RedirectToAction("Update", new { userName = UserName });
        }



        #endregion

        #region View User Details Methods

        [HttpGet]
        public ActionResult Update(string userName)
        {
            MembershipUser user = membershipService.GetUser(userName);

            UserViewModel viewModel = new UserViewModel();
            viewModel.User = user;
            viewModel.RequiresSecretQuestionAndAnswer = membershipService.RequiresQuestionAndAnswer;
            viewModel.Roles = roleService.GetRolesForUser(userName);

            return View(viewModel);
        }

        [HttpPost]
        //[ActionName("Update")]
        [MultiButtonFormSubmit(ActionName = "UpdateDeleteCancel", SubmitButton = "UpdateUser")]
        public ActionResult UpdateUser(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                throw new ArgumentNullException("userName");
            }

            MembershipUser user = membershipService.GetUser(UserName);

            try
            {
                user.Comment = Request["User.Comment"];
                user.Email = Request["User.Email"];

                membershipService.UpdateUser(user);
                TempData["SuccessMessage"] = "The user was updated successfully!";

            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "There was an error updating this user.";
            }

            return RedirectToAction("Update", new { userName = user.UserName });
        }


        #region Ajax methods for Updating the user

        [HttpPost]
        public ActionResult Unlock(string userName)
        {
            JsonResponse response = new JsonResponse();

            MembershipUser user = membershipService.GetUser(userName);

            try
            {
                user.UnlockUser();
                response.Success = true;
                response.Message = "User unlocked successfully!";
                response.Locked = false;
                response.LockedStatus = (response.Locked) ? "Locked" : "Unlocked";
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "User unlocked failed.";
            }

            return Json(response);
        }

        [HttpPost]
        public ActionResult ApproveDeny(string userName)
        {
            JsonResponse response = new JsonResponse();

            MembershipUser user = membershipService.GetUser(userName);

            try
            {
                user.IsApproved = !user.IsApproved;
                membershipService.UpdateUser(user);

                string approvedMsg = (user.IsApproved) ? "Approved" : "Denied";

                response.Success = true;
                response.Message = "User " + approvedMsg + " successfully!";
                response.Approved = user.IsApproved;
                response.ApprovedStatus = (user.IsApproved) ? "Approved" : "Not approved";
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "User unlocked failed.";
            }

            return Json(response);
        }

        #endregion

        #endregion

        #region Cancel User Methods

        [HttpPost]
        [MultiButtonFormSubmit(ActionName = "UpdateDeleteCancel", SubmitButton = "UserCancel")]
        public ActionResult Cancel()
        {
            return RedirectToAction("Index");
        }

        #endregion



        #region Grant Users with Roles Methods

        /// <summary>
        /// Return two lists:
        ///   1)  a list of Roles not granted to the user
        ///   2)  a list of Roles granted to the user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public virtual ActionResult GrantRolesToUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index");
            }

            GrantRolesToUserViewModel model = new GrantRolesToUserViewModel();
            model.UserName = username;
            model.AvailableRoles = (string.IsNullOrEmpty(username) ? new SelectList(roleService.GetAllRoles()) : new SelectList(roleService.AvailableRolesForUser(username)));
            model.GrantedRoles = (string.IsNullOrEmpty(username) ? new SelectList(new string[] { }) : new SelectList(roleService.GetRolesForUser(username)));

            return View(model);
        }

        /// <summary>
        /// Grant the selected roles to the user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleNames"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GrantRolesToUser(string userName, string roles)
        {
            JsonResponse response = new JsonResponse();

            if (string.IsNullOrEmpty(userName))
            {
                response.Success = false;
                response.Message = "The userName is missing.";
                return Json(response);
            }

            string[] roleNames = roles.Substring(0, roles.Length - 1).Split(',');

            if (roleNames.Length == 0)
            {
                response.Success = false;
                response.Message = "No roles have been granted to the user.";
                return Json(response);
            }

            try
            {
                roleService.AddUserToRoles(userName, roleNames);

                response.Success = true;
                response.Message = "The Role(s) has been GRANTED successfully to " + userName;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "There was a problem adding the user to the roles.";
            }

            return Json(response);
        }

        /// <summary>
        /// Revoke the selected roles for the user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleNames"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RevokeRolesForUser(string userName, string roles)
        {
            JsonResponse response = new JsonResponse();

            if (string.IsNullOrEmpty(userName))
            {
                response.Success = false;
                response.Message = "The userName is missing.";
                return Json(response);
            }

            if (string.IsNullOrEmpty(roles))
            {
                response.Success = false;
                response.Message = "Roles is missing";
                return Json(response);
            }

            string[] roleNames = roles.Substring(0, roles.Length - 1).Split(',');

            if (roleNames.Length == 0)
            {
                response.Success = false;
                response.Message = "No roles are selected to be revoked.";
                return Json(response);
            }

            try
            {
                roleService.RemoveUserFromRoles(userName, roleNames);

                response.Success = true;
                response.Message = "The Role(s) has been REVOKED successfully for " + userName;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "There was a problem revoking roles for the user.";
            }

            return Json(response);
        }

        #endregion

    }
}
