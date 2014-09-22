using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace SecurityGuard.ViewModels
{
    public class GrantRolesToUserViewModel
    {
        public MembershipUser User { get; set; }
        public string UserName { get; set; }
        public SelectList AvailableRoles { get; set; }
        public SelectList GrantedRoles { get; set; }
    }
}
