namespace FasTnT.Domain.Model.Events
{
    public class CustomField
    {
        public FieldType Type { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Value { get; set; }
    }

    public enum FieldType
    {
        Ilmd,
        EventExtension,
        BusinessLocationExtension,
        ErrorDeclarationExtension,
        ReadPointExtension
    }
}
