using System;

namespace Epcis.Model.Exceptions
{
    [Serializable]
    public class SubscribeNotPermittedException : EpcisException
    {
        public SubscribeNotPermittedException(string queryName) : base(string.Format("Query '{0}' does not allow subscription", queryName))
        {
        }
    }
}