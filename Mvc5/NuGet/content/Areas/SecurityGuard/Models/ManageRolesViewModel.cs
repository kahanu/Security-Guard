using System.Collections.Generic;
using System.Web.Mvc;

namespace SecurityGuard.ViewModels
{
    public class ManageRolesViewModel
    {
        public SelectList Roles { get; set; }
        public string[] RoleList { get; set; }
    }
}
