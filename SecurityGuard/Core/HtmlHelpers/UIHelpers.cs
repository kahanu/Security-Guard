using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace SecurityGuard.Core.HtmlHelpers
{
    public static class UIHelpers
    {
        public static MvcHtmlString SGActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result)
        {
            return htmlHelper.RouteLink(linkText, result.GetRouteValueDictionary());
        }

        public static MvcHtmlString SGActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, object htmlAttributes)
        {
            return SGActionLink(htmlHelper, linkText, result, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString SGActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.RouteLink(linkText, result.GetRouteValueDictionary(), htmlAttributes);
        }

        public static IActionResult GetMVCResult(this ActionResult result)
        {
            var t4MVCResult = result as IActionResult;
            if (t4MVCResult == null)
            {
                throw new InvalidOperationException("An incorrect ActionResult was returned.");
            }
            return t4MVCResult;
        }

        public static RouteValueDictionary GetRouteValueDictionary(this ActionResult result)
        {
            return result.GetMVCResult().RouteValueDictionary;
        }
    }

    public interface IActionResult
    {
        string Action { get; set; }
        string Controller { get; set; }
        RouteValueDictionary RouteValueDictionary { get; set; }
    }
}
