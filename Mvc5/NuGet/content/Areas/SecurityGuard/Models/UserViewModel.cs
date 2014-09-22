using System.Web.Security;

namespace SecurityGuard.ViewModels
{
    public class UserViewModel
    {
        public MembershipUser User { get; set; }
        public bool RequiresSecretQuestionAndAnswer { get; set; }
        public string[] Roles { get; set; }
    }
}
