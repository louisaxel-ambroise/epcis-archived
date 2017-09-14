using System.Web.Optimization;

namespace FasTnT.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/materialize").Include("~/Scripts/materialize.js","~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/morrisjs").Include("~/Scripts/raphael.min.js", "~/Scripts/morris.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/materialize.css", "~/Content/site.css"));
        }
    }
}
