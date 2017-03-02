using Dapper;

namespace Epcis.Data.Infrastructure
{
    public static class StorageExtensions
    {
        public static void Setup()
        {
            SqlMapper.AddTypeHandler(new TimeZoneOffsetHandler());
        }
    }
}