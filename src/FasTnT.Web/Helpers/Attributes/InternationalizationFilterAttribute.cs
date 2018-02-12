using FasTnT.Web.Helpers;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace FasTnT.Web.Helpers.Attributes
{
    public class InternationalizationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string language = filterContext.HttpContext.Request.Cookies[Constants.PreferredLanguage]?.Value 
                                ?? (string)filterContext.HttpContext.Session[Constants.PreferredLanguage] 
                                ?? "en";

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        }
    }
}