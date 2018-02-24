using System.Collections.Generic;

namespace FasTnT.Domain.Model.MasterData
{
    public class MasterdataAttribute
    {
        public virtual EpcisMasterdata MasterData { get; set; }
        public virtual string Id { get; set; }
        public virtual string Value { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as MasterdataAttribute;
            return other != null && EqualityComparer<EpcisMasterdata>.Default.Equals(MasterData, other.MasterData) && Id == other.Id;
        }

        public override int GetHashCode()
        {
            var hashCode = 683338227;
            hashCode = hashCode * -1521134295 + EqualityComparer<EpcisMasterdata>.Default.GetHashCode(MasterData);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            return hashCode;
        }
    }
}
