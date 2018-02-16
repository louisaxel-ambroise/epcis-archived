using FasTnT.Domain.Services.Dashboard.Users;
using FasTnT.Web.Features;
using FasTnT.Web.Helpers;
using FasTnT.Web.Session;
using System.Web;
using System.Web.Mvc;

namespace FasTnT.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var feature = new WebDashboardFeature();
            if (feature.IsOn())
            {
                return RedirectToAction("LogOn", "Account");
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SetLanguage(string language, string redirectTo)
        {
            if (Request.LogonUserIdentity.IsAuthenticated)
            {
                _userService.SetPreferredLanguage(UserSession.Current.Id, language);
            }

            Session[Constants.PreferredLanguage] = language;
            Response.Cookies.Set(new HttpCookie(Constants.PreferredLanguage, language));

            return Redirect(redirectTo);
        }
    }
}