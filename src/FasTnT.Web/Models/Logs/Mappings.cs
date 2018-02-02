using FasTnT.Domain.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Web.Models.Logs
{
    public static class Mappings
    {
        public static IEnumerable<AuditLogSummary> MapToAuditLogSummary(this IEnumerable<AuditLog> logs)
        {
            return logs.Select(x => new AuditLogSummary
            {
                Id = x.Id,
                Action = x.Action.ToString(),
                Timestamp = x.Timestamp,
                Description = x.Description,
                Status = GetStatus(x.Type),
                UserId = x.User != null ? x.User.Id : default(Guid?),
                UserName = x.User != null ? x.User.Name : null,
                ExecutionTimeMs = x.ExecutionTimeMs
            });
        }

        private static string GetStatus(string type)
        {
            switch (type)
            {
                case "Warning": return "timer";
                case "Error": return "error";
                default: return string.Empty;
            }
        }
    }
}