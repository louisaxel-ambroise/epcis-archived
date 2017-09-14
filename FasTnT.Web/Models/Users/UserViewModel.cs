using System;

namespace FasTnT.Web.Models.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime? LastLogon { get; set; }
    }
}