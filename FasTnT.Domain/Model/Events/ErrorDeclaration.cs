using System;

namespace FasTnT.Domain.Model.Events
{
    public class ErrorDeclaration
    {
        public DateTime DeclarationTime { get; internal set; }
        public string Reason { get; internal set; }
    }
}
