using System;

namespace FasTnT.Web.Models.Masterdata
{
    public class MasterdataViewModel
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int AttributesCount { get; set; }
    }
}