using FasTnT.Domain.Model.Users;
using System;

namespace FasTnT.Domain.Model.Log
{
    public class AuditLog
    {
        public virtual Guid Id { get; set; }
        public virtual User User { get; set; }
        public virtual LogAction Action { get; set; }
        public virtual DateTime Timestamp { get; set; }
        public virtual string Description { get; set; }
        public virtual long ExecutionTimeMs { get; set; }
        public virtual string Type { get; set; }
    }
}
