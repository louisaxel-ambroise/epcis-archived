using FasTnT.Web.Models.Events;
using System.Web.Mvc;

namespace FasTnT.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string eventId)
        {
            return PartialView(new EventDetail
            {
                EventId = eventId
            });
        }
    }
}