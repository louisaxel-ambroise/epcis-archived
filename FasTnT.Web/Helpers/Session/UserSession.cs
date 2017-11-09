using FasTnT.Web.Models.Users;
using Newtonsoft.Json;
using System.Web;
using System.Web.Security;

namespace FasTnT.Web.Session
{
    public class UserSession
    {
        private const string WebUserKey = "FasTnT.Web.User.Key";

        public static WebUser Current
        {
            get { return HttpContext.Current.Session[WebUserKey] as WebUser ?? (HttpContext.Current.Session[WebUserKey] = AuthorizeFromCookie()) as WebUser; }
            set { HttpContext.Current.Session[WebUserKey] = value; }
        }

        public static bool IsAuthenticated()
        {
            return Current != null || (Current = AuthorizeFromCookie()) != null;
        }

        private static WebUser AuthorizeFromCookie()
        {
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null) return null;

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            return JsonConvert.DeserializeObject<WebUser>(ticket.UserData);
        }
    }
}