using System.Web.Optimization;

namespace FasTnT.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/insite_css").Include("~/Content/desktop.css", "~/Content/desktop_table.css"));
            bundles.Add(new StyleBundle("~/Content/logon_css").Include("~/Content/logon.css"));
        }
    }
}
