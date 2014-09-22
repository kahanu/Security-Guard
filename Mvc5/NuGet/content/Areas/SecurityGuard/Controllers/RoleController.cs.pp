using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using SecurityGuard.Services;
using SecurityGuard.Interfaces;
using SecurityGuard.ViewModels;
using $rootnamespace$.Controllers;

namespace $rootnamespace$.Areas.SecurityGuard.Controllers
{
    [Authorize(Roles="SecurityGuard")]
    public partial class RoleController : BaseController
    {

        #region ctors

        private readonly IRoleService roleService;

        public RoleController()
        {
            this.roleService = new RoleService(Roles.Provider);
        }

        #endregion

        public virtual ActionResult Index()
        {
            ManageRolesViewModel model = new ManageRolesViewModel();
            model.Roles = new SelectList(roleService.GetAllRoles());
            model.RoleList = roleService.GetAllRoles();

            return View(model);
        }

        #region Create Roles Methods

        [HttpGet]
        public virtual ActionResult CreateRole()
        {
            return View(new RoleViewModel());
        }

        [HttpPost]
        public virtual ActionResult CreateRole(string roleName)
        {
            JsonResponse response = new JsonResponse();

            if (string.IsNullOrEmpty(roleName))
            {
                response.Success = false;
                response.Message = "You must enter a role name.";
                response.CssClass = "red";

                return Json(response);
            }

            try
            {
                roleService.CreateRole(roleName);

                if (Request.IsAjaxRequest())
                {
                    response.Success = true;
                    response.Message = "Role created successfully!";
                    response.CssClass = "green";

                    return Json(response);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (Request.IsAjaxRequest())
                {
                    response.Success = false;
                    response.Message = ex.Message;
                    response.CssClass = "red";

                    return Json(response);
                }

                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete Roles Methods

        /// <summary>
        /// This is an Ajax method.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult DeleteRole(string roleName)
        {
            JsonResponse response = new JsonResponse();

            if (string.IsNullOrEmpty(roleName))
            {
                response.Success = false;
                response.Message = "You must select a Role Name to delete.";
                response.CssClass = "red";

                return Json(response);
            }

            roleService.DeleteRole(roleName);

            response.Success = true;
            response.Message = roleName + " was deleted successfully!";
            response.CssClass = "green";

            return Json(response);
        }

        [HttpPost]
        public ActionResult DeleteRoles(string roles, bool throwOnPopulatedRole)
        {
            JsonResponse response = new JsonResponse();
            response.Messages = new List<ResponseItem>();

            if (string.IsNullOrEmpty(roles))
            {
                response.Success = false;
                response.Message = "You must select at least one role.";
                return Json(response);
            }

            string[] roleNames = roles.Split(',');
            StringBuilder sb = new StringBuilder();

            ResponseItem item = null;

            foreach (var role in roleNames)
            {
                if (!string.IsNullOrEmpty(role))
                {
                    try
                    {
                        roleService.DeleteRole(role, throwOnPopulatedRole);

                        item = new ResponseItem();
                        item.Success = true;
                        item.Message = "Deleted this role successfully - " + role;
                        item.CssClass = "green";
                        response.Messages.Add(item);

                        //sb.AppendLine("Deleted this role successfully - " + role + "<br />");
                    }
                    catch (System.Configuration.Provider.ProviderException ex)
                    {
                        //sb.AppendLine(role + " - " + ex.Message + "<br />");

                        item = new ResponseItem();
                        item.Success = false;
                        item.Message = ex.Message;
                        item.CssClass = "yellow";
                        response.Messages.Add(item);
                    }
                }
            }

            response.Success = true;
            response.Message = sb.ToString();

            return Json(response);
        }

        #endregion

        #region Get Users In Role methods

        /// <summary>
        /// This is an Ajax method that populates the 
        /// Roles drop down list.
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllRoles()
        {
            var list = roleService.GetAllRoles();

            List<SelectObject> selectList = new List<SelectObject>();

            foreach (var item in list)
            {
                selectList.Add(new SelectObject() { caption = item, value = item });
            }

            return Json(selectList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetUsersInRole(string roleName)
        {
            var list = roleService.GetUsersInRole(roleName);

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}
