using FasTnT.Domain.Exceptions;
using FasTnT.Domain.Services.Dashboard;
using FasTnT.Domain.Services.Dashboard.Users;
using FasTnT.Domain.Utils;
using FasTnT.Web.Helpers;
using FasTnT.Web.Helpers.Attributes;
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

        [NoCache]
        [AllowAnonymous]
        public ActionResult LogOn(string returnUrl)
        {
            if (UserSession.IsAuthenticated())
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.ReturnUrl = returnUrl;
            var model = new AuthenticationModel();
            var lockedUser = RetrieveLockedUser();

            if (lockedUser != null)
            {
                model.UserName = lockedUser;
                model.Locked = true;
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn(AuthenticationModel credentials, string returnUrl)
        {
            try
            {
                var user = _userAuthenticator.Authenticate(credentials.UserName, credentials.Password);
                var webUser = WebUser.Create(user);

                GenerateAndStoreCookies(webUser);
                UserSession.Current = webUser;

                return !string.IsNullOrEmpty(returnUrl) ? (ActionResult) Redirect(returnUrl) : RedirectToAction("Index", "Dashboard");
            }
            catch(UserAuthenticationException authFailure)
            {
                credentials.AuthenticationError = authFailure.FailureReason.ToString();

                return View(credentials);
            }
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            DeleteCookies();

            return RedirectToAction("LogOn");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var userName = HttpContext.User.Identity.Name;
            var userDetails = _userService.GetDetails(userName);

            return View(userDetails.MapToViewModel());
        }

        private string RetrieveLockedUser()
        {
            var cookie = Request.Cookies["fastnt.username"];

            return (cookie != null) ? cookie.Value : null;
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
            Response.Cookies.Add(new HttpCookie("fastnt.username", user.Name) { HttpOnly = true, Expires = SystemContext.Clock.Now.AddDays(1) });
        }

        private void DeleteCookies()
        {
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "") { Expires = DateTime.Now.AddYears(-1) };
            var lockCookie = new HttpCookie("fastnt.username", "") { Expires = DateTime.Now.AddYears(-1) };

            Response.Cookies.Add(authCookie);
            Response.Cookies.Add(lockCookie);
        }
    }
}