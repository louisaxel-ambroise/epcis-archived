using System.Web.Mvc;

namespace FasTnT.Web.Controllers
{
    public class AboutController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}