using System.Web.Mvc;
using System.Web.Security;
using SecurityGuard.Services;
using SecurityGuard.Interfaces;
using SecurityGuard.ViewModels;
using $rootnamespace$.Controllers;

namespace $rootnamespace$.Areas.SecurityGuard.Controllers
{
    [Authorize(Roles = "SecurityGuard")]
    public partial class DashboardController : BaseController
    {

        #region ctors

        private IMembershipService membershipService;
        private IRoleService roleService;

        public DashboardController()
        {
            this.roleService = new RoleService(Roles.Provider);
            this.membershipService = new MembershipService(Membership.Provider);
        }

        #endregion


        public virtual ActionResult Index()
        {
            DashboardViewModel viewModel = new DashboardViewModel();
            int totalRecords;

            membershipService.GetAllUsers(0, 20, out totalRecords);
            viewModel.TotalUserCount = totalRecords.ToString();
            viewModel.TotalUsersOnlineCount = membershipService.GetNumberOfUsersOnline().ToString();
            viewModel.TotalRolesCount = roleService.GetAllRoles().Length.ToString();

            return View(viewModel);
        }

    }
}
