using System;

namespace FasTnT.Web.Models.Users
{
    public class ApiUserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? LastLogon { get; set; }
        public bool IsActive { get; set; }
        public int SubscriptionCount { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}