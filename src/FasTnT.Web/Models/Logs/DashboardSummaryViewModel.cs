using System.Collections.Generic;

namespace FasTnT.Web.Models.Logs
{
    public class DashboardSummaryViewModel
    {
        public IEnumerable<AuditLogSummary> Logs { get; set; }
        public int TotalLogs { get; set; }
    }
}