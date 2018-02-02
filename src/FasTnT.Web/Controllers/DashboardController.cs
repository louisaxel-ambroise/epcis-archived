using FasTnT.Domain.Repositories;
using FasTnT.Web.Models.Logs;
using System.Linq;
using System.Web.Mvc;

namespace FasTnT.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public DashboardController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public ActionResult Index()
        {
            var logs = _auditLogRepository.Query()
                .OrderByDescending(x => x.Timestamp).Take(10)
                .MapToAuditLogSummary();

            return View(logs);
        }
    }
}