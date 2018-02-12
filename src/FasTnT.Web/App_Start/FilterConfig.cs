using FasTnT.Web.Helpers.Attributes;
using System.Web.Mvc;

namespace FasTnT.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new InternationalizationFilterAttribute());
        }
    }
}
