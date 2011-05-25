using System.Web.Mvc;
using System.Web.Security;
using SecurityGuard.Services;
using SecurityGuard.Interfaces;
using SecurityGuard.ViewModels;
using SecurityGuard.Controllers;

namespace SecurityGuard.Areas.SGMembership.Controllers
{
    [Authorize(Roles = "SecurityGuard")]
    public partial class DashboardController : BaseController
    {

        #region ctors

        private IMembershipService membershipService;
        private IRoleService roleService;

        //public DashboardController(IMembershipService membershipService, IRoleService roleService)
        //{
        //    this.roleService = roleService;
        //    this.membershipService = membershipService;
        //}

        //public DashboardController()
        //    : this(new MembershipService(Membership.Provider),
        //    new RoleService(Roles.Provider))
        //{

        //}

        public DashboardController()
        {
            this.roleService = new RoleService(Roles.Provider);
            this.membershipService = new MembershipService(Membership.Provider);
        }

        #endregion

        //
        // GET: /SecurityGuard/Dashboard/

        public virtual ActionResult Index()
        {
            DashboardViewModel viewModel = new DashboardViewModel();
            viewModel.TotalUserCount = membershipService.GetAllUsers().Count.ToString();
            viewModel.TotalUsersOnlineCount = membershipService.GetNumberOfUsersOnline().ToString();
            viewModel.TotalRolesCount = roleService.GetAllRoles().Length.ToString();

            return View(viewModel);
        }

    }
}
