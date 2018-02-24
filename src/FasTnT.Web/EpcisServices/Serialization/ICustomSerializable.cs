using System.IO;

namespace FasTnT.Web.EpcisServices
{
    public interface ICustomSerializable
    {
        void WriteTo(Stream stream);
        void InitializeFrom(Stream stream);
    }
}
