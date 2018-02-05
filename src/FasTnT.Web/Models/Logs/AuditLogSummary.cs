using System;

namespace FasTnT.Web.Models.Logs
{
    public class AuditLogSummary
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
        public Guid? UserId { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public long ExecutionTimeMs { get; set; }
    }
}