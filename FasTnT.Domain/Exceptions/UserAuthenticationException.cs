using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace FasTnT.Domain.Exceptions
{
    [Serializable]
    public class UserAuthenticationException : Exception
    {
        public Failure FailureReason { get; internal set; }

        public UserAuthenticationException(Failure failure)
        {
            FailureReason = failure;
        }

        protected UserAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info == null) throw new ArgumentNullException("info");

            FailureReason = (Failure) Enum.Parse(typeof(Failure), info.GetString("FailureReason"));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("FailureReason", FailureReason.ToString());
        }

        public enum Failure
        {
            UnknownUser,
            WrongPassword,
            UnknownError,
            AccessDenied
        }
    }
}
