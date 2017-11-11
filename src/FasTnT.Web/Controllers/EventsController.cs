using FasTnT.Domain.Repositories;
using FasTnT.Web.Models.Events;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FasTnT.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository ?? throw new ArgumentException(nameof(eventRepository));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read_LatestEvents()
        {
            var latestEvents = _eventRepository.Query().OrderByDescending(e => e.Request.RecordTime).Take(5).MapToEventSummary();
            var totalEvents = _eventRepository.Query().Count();

            return PartialView("_LatestEvents", new LatestEventsViewModel { Events = latestEvents.ToArray(), Total = totalEvents });
        }

        public ActionResult Details(Guid id)
        {
            var @event = _eventRepository.LoadById(id).MapToEventDetail();

            return View(@event);
        }
    }
}