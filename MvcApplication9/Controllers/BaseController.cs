using System;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication9.Controllers
{
    public partial class BaseController : Controller
    {
        /// <summary>
        /// This provides simple feedback to the modelstate in the case of errors.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is RedirectToRouteResult)
            {
                try
                {
                    // put the ModelState into TempData
                    TempData.Add("_MODELSTATE", ModelState);
                }
                catch (Exception)
                {
                    TempData.Clear();
                    // swallow exception
                }
            }
            else if (filterContext.Result is ViewResult && TempData.ContainsKey("_MODELSTATE"))
            {
                // merge modelstate from TempData
                var modelState = TempData["_MODELSTATE"] as ModelStateDictionary;
                if (modelState != null)
                    foreach (var item in modelState.Where(item => !ModelState.ContainsKey(item.Key)))
                    {
                        ModelState.Add(item);
                    }
            }
            base.OnActionExecuted(filterContext);
        }

    }
}
