using FasTnT.Domain.Model.Subscriptions;
using FasTnT.Domain.Utils.Security;
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.Users
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Mail { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string PasswordSalt { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string PreferredLanguage { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual DateTime? LastLogOn { get; set; }
        public virtual UserType Role { get; set; }

        public virtual IList<UserLog> Logs { get; set; }
        public virtual IList<Subscription> Subscriptions { get; set; }

        public virtual bool VerifyPassword(string password)
        {
            return BCrypt.CheckPassword(password, PasswordHash, PasswordSalt);
        }
    }
}
