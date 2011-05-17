using System.Web.Mvc;
using System.Web.Routing;

namespace SecurityGuard.Core.RouteHelpers
{
    public class Actions
    {

        #region Membership Action Helpers

        public static RedirectToRouteResult GrantRolesToUser(string userName)
        {
            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "GrantRolesToUser", controller = "Membership", username = userName }));
        }

        

        #endregion
    }
}
