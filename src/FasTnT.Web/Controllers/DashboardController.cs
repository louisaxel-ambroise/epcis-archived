using FasTnT.Domain.Repositories;
using FasTnT.Web.Models.Logs;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ActionResult> Index()
        {
            var count = Task.Run(() => _auditLogRepository.Query().Count());
            var logs = Task.Run(() => _auditLogRepository.Query()
                .OrderByDescending(x => x.Timestamp).Take(5)
                .MapToAuditLogSummary());

            return View(new DashboardSummaryViewModel
            {
                Logs = await logs,
                TotalLogs = await count
            });
        }
    }
}