using System.Web.Mvc;

namespace FasTnT.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            // TODO: fetch last x events (x configurable in database)
            return View();
        }
    }
}