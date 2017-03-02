namespace Epcis.Services.Capture
{
    public interface ICapturer<in T>
    {
        void Capture(T document);
    }
}