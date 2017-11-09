using FasTnT.Web.Internationalization;
using System.Collections;
using System.Linq;
using System.Web.Mvc;

namespace FasTnT.Web.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString AssemblyVersion(this HtmlHelper htmlHelper, int fieldCount = 4)
        {
            var version = typeof(HtmlExtensions).Assembly.GetName().Version.ToString(fieldCount);

            return new MvcHtmlString(version);
        }

        public static MvcHtmlString LocalizeGrid(this HtmlHelper htmlHelper, string key)
        {
            return htmlHelper.Localize($"Grid_{key}");
        }

        public static MvcHtmlString LocalizeLabel(this HtmlHelper htmlHelper, string key)
        {
            return htmlHelper.Localize($"Label_{key}");
        }

        public static string LocalizeFormat(this HtmlHelper htmlHelper, string key)
        {
            return htmlHelper.Localize($"Format_{key}").ToHtmlString();
        }

        public static string LocalizeChart(this HtmlHelper htmlHelper, string key)
        {
            return htmlHelper.Localize($"Chart_{key}").ToHtmlString();
        }

        public static MvcHtmlString Localize(this HtmlHelper htmlHelper, string key)
        {
            var rm = new System.Resources.ResourceManager("FasTnT.Web.Internationalization.Resources", typeof(Resources).Assembly);
            var culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            var entry = rm.GetResourceSet(culture, true, true).OfType<DictionaryEntry>().FirstOrDefault(e => e.Key.ToString() == key);

            return MvcHtmlString.Create(entry.Value?.ToString() ?? key);
        }
    }
}