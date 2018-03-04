using System;
using System.Collections.Generic;

namespace FasTnT.Web.Models.Users
{
    public class ApiUserDetailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? LastLogon { get; set; }
        public bool IsActive { get; set; }
        public IList<SubscriptionViewModel> Subscriptions { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }

    public class SubscriptionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string QueryName { get; set; }
        public string Schedule { get; set; }
    }
}