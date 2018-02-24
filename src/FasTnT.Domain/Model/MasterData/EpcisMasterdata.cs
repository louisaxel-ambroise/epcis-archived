using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.MasterData
{
    public class EpcisMasterdata
    {
        public EpcisMasterdata()
        {
            Attributes = new List<MasterdataAttribute>();
        }

        public virtual string Type { get; set; }
        public virtual string Id { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual DateTime LastUpdatedOn { get; set; }

        public virtual IList<MasterdataAttribute> Attributes { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as EpcisMasterdata;

            return other != null && Equals(Id, other.Id) && Equals(Type, other.Type);
        }

        public override int GetHashCode()
        {
            var hashCode = -1324594315;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            return hashCode;
        }
    }
}
