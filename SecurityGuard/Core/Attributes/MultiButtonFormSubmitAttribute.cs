using System;
using System.Reflection;
using System.Web.Mvc;

namespace SecurityGuard.Core.Attributes
{
    public class MultiButtonFormSubmitAttribute : ActionNameSelectorAttribute
    {
        public string SubmitButton { get; set; }
        public string ActionName { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            if (!string.Equals(actionName, ActionName, StringComparison.OrdinalIgnoreCase))
                return false;

            var o = controllerContext.HttpContext.Request[SubmitButton];
            if (o == null)
                return false;

            controllerContext.RouteData.Values["action"] = methodInfo.Name;
            return true;
        }
    }
}
