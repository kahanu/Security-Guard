using System.Web.Mvc;

namespace $rootnamespace$.Areas.SecurityGuard
{
    public class SecurityGuardAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SecurityGuard";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute("SearchMembership", "SecurityGuard/Membership/index/{searchterm}/{filterby}",
                new { controller = "Membership", action = "Index", searchterm = UrlParameter.Optional, filterby = "all" }
                );

            context.MapRoute("Membership", "SecurityGuard/Membership/{action}/{userName}",
                new { controller = "Membership", userName = UrlParameter.Optional }
                );  

            context.MapRoute(
                "SecurityGuard_default",
                "SecurityGuard/{controller}/{action}/{id}",
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
