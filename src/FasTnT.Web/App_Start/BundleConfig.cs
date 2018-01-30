using System.Web.Optimization;

namespace FasTnT.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Style bundles
            bundles.Add(new StyleBundle("~/Content/insite_css").Include("~/Content/desktop/*.css", "~/Content/mobile/*.css"));
            bundles.Add(new StyleBundle("~/Content/logon_css").Include("~/Content/logon.css"));

            // Script bundles
            bundles.Add(new ScriptBundle("~/Scripts/fastnt").Include("~/Scripts/fastnt.js"));
            bundles.Add(new ScriptBundle("~/Scripts/constants").Include("~/Scripts/constants.js"));
        }
    }
}
