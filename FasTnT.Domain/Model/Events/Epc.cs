namespace FasTnT.Domain.Model.Events
{
    public class Epc
    {
        public string Id { get; set; }
        public EpcType Type { get; set; }
        public bool IsQuantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public float? Quantity { get; set; }
    }
}