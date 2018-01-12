using FasTnT.Domain.Exceptions;
using FasTnT.Domain.Services.Dashboard;
using FasTnT.Domain.Services.Dashboard.Users;
using FasTnT.Domain.Utils;
using FasTnT.Web.Helpers;
using FasTnT.Web.Models.Account;
using FasTnT.Web.Models.Users;
using FasTnT.Web.Session;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FasTnT.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserAuthenticator _userAuthenticator;
        private readonly IUserService _userService;

        public AccountController(IUserAuthenticator userAuthenticator, IUserService userService)
        {
            _userAuthenticator = userAuthenticator;
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult LogOn()
        {
            if (UserSession.IsAuthenticated())
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View(new AuthenticationModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn(AuthenticationModel credentials)
        {
            try
            {
                var user = _userAuthenticator.Authenticate(credentials.UserName, credentials.Password);
                var webUser = WebUser.Create(user);

                GenerateAndStoreCookies(webUser);
                UserSession.Current = webUser;

                return RedirectToAction("Index", "Dashboard");
            }
            catch(UserAuthenticationException authFailure)
            {
                credentials.AuthenticationError = authFailure.FailureReason.ToString();

                return View(credentials);
            }
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

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            DeleteCookie();

            return RedirectToAction("LogOn");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var userName = HttpContext.User.Identity.Name;
            var userDetails = _userService.GetDetails(userName);

            return View(userDetails.MapToViewModel());
        }

        private void GenerateAndStoreCookies(WebUser user)
        {
            // Set authorization cookie
            var ticket = new FormsAuthenticationTicket(1, user.Name, SystemContext.Clock.Now, SystemContext.Clock.Now.AddMinutes(30), false, JsonConvert.SerializeObject(user), FormsAuthentication.FormsCookiePath);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)) { HttpOnly = true };

            // Set language cookie if needed
            if (!string.IsNullOrEmpty(user.PreferredLanguage))
            {
                var langCookie = new HttpCookie(Constants.PreferredLanguage, user.PreferredLanguage) { Expires = SystemContext.Clock.Now.AddYears(1) };

                Response.Cookies.Add(langCookie);
                Session[Constants.PreferredLanguage] = user.PreferredLanguage;
            }

            Response.Cookies.Add(authCookie);
        }

        private void DeleteCookie()
        {
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "") { Expires = DateTime.Now.AddYears(-1) };

            Response.Cookies.Add(cookie);
        }
    }
}