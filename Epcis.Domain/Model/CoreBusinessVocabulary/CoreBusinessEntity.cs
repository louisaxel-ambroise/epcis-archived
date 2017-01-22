namespace Epcis.Domain.Model.CoreBusinessVocabulary
{
    public abstract class CoreBusinessEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
    }
}