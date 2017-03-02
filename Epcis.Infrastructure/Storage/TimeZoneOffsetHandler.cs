using System.Data;
using Dapper;
using Epcis.Model.Events;

namespace Epcis.Infrastructure.Storage
{
    public class TimeZoneOffsetHandler : SqlMapper.TypeHandler<TimeZoneOffset>
    {
        public override void SetValue(IDbDataParameter parameter, TimeZoneOffset value)
        {
            parameter.Value = value.Value;
        }

        // TimeZoneOffset is stored as tinyint in database
        public override TimeZoneOffset Parse(object value)
        {
            return new TimeZoneOffset((short)value);
        }
    }
}