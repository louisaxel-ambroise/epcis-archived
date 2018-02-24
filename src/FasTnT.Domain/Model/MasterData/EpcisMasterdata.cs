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
    }
}
