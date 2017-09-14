using System.Web.Mvc;

namespace FasTnT.Web.Controllers
{
    [Authorize]
    public class EpcisServerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}