using System;

namespace FasTnT.Domain.Model.Events
{
    public class ErrorDeclaration
    {
        public virtual DateTime DeclarationTime { get; internal set; }
        public virtual string Reason { get; internal set; }
    }
}
