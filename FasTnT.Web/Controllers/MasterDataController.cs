using System.Web.Mvc;

namespace FasTnT.Web.Controllers
{
    [Authorize]
    public class MasterDataController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}