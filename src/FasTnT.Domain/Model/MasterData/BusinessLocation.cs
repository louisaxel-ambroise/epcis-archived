using System;

namespace FasTnT.Domain.Model.MasterData
{
    public class BusinessLocation
    {
        public virtual string Id { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual DateTime LastUpdated { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string Country { get; set; }
        public virtual float? Latitude { get; set; }
        public virtual float? Longitude { get; set; }
    }
}
