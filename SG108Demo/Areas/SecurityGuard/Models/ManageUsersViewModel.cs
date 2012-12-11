using System.Web.Security;
using SecurityGuard.Core.Pagination;

namespace SecurityGuard.ViewModels
{
    public class ManageUsersViewModel
    {
        public MembershipUserCollection Users { get; set; }
        public PaginatedList<MembershipUser> PaginatedUserList { get; set; }
        public string FilterBy { get; set; }
        public string SearchTerm { get; set; }
        public int PageSize { get; set; }
    }
}
