using Dapper;

namespace Epcis.Infrastructure.Storage
{
    public static class StorageExtensions
    {
        public static void Setup()
        {
            SqlMapper.AddTypeHandler(new TimeZoneOffsetHandler());
        }
    }
}