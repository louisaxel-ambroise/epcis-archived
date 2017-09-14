using System;

namespace FasTnT.Domain.Model.Users
{
    public class UserLog
    {
        public virtual Guid Id { get; set; }
        public virtual User AppliesTo { get; set; }
        public virtual UserLogType Type { get; set; }
        public virtual string Payload { get; set; }
        public virtual DateTime Timestamp { get; set; }
    }
}
