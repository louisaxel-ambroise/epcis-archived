namespace Epcis.Model.Query
{
    public class EpcisQuery<T>
    {
        public string Name { get; set; }
        public T Parameters { get; set; }
    }
}