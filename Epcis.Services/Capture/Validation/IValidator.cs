namespace Epcis.Services.Capture.Validation
{
    public interface IValidator<in T>
    {
        void Validate(T input);
    }
}