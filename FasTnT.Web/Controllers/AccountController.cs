using FasTnT.Domain.Exceptions;
using FasTnT.Domain.Model.Users;
using FasTnT.Domain.Services.Dashboard;
using FasTnT.Domain.Utils;
using FasTnT.Web.Helpers;
using FasTnT.Web.Models.Account;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FasTnT.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IWebUserAuthenticator _userAuthenticator;

        public AccountController(IWebUserAuthenticator userAuthenticator)
        {
            _userAuthenticator = userAuthenticator;
        }

        [AllowAnonymous]
        public ActionResult LogOn()
        {
            if (Request.LogonUserIdentity.IsAuthenticated)
            {
                return Redirect("~/");
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
                GenerateAndStoreCookies(user);

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

        private void GenerateAndStoreCookies(User user)
        {
            // Set authorization cookie
            var ticket = new FormsAuthenticationTicket(1, user.Name, SystemContext.Clock.Now, SystemContext.Clock.Now.AddMinutes(30), false, user.Name, FormsAuthentication.FormsCookiePath);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)) { HttpOnly = true };

            // Set language cookie if needed
            if (!string.IsNullOrEmpty(user.PreferredLanguage))
            {
                var langCookie = new HttpCookie(Constants.PreferredLanguage, user.PreferredLanguage);
                langCookie.Expires = SystemContext.Clock.Now.AddYears(1);

                Response.Cookies.Add(langCookie);
                Session[Constants.PreferredLanguage] = user.PreferredLanguage;
            }

            Response.Cookies.Add(authCookie);
        }

        private void DeleteCookie()
        {
            var cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);

            Response.Cookies.Add(cookie1);
        }
    }
}