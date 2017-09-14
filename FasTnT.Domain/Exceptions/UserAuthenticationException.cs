using System;

namespace FasTnT.Domain.Exceptions
{
    public class UserAuthenticationException : Exception
    {
        public Failure FailureReason { get; internal set; }

        public UserAuthenticationException(Failure failure)
        {
            FailureReason = failure;
        }

        public enum Failure
        {
            UnknownUser, WrongPassword,
            UnknownError,
            AccessDenied
        }
    }
}
