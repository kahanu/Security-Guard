using System.Web.Mvc;
using System.Web.Routing;

namespace SecurityGuard.Core.RouteHelpers
{
    public static class Area
    {
        public static class Membership
        {
            public static ActionResult Index()
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Membership" }));
            }
        }
    }
}
