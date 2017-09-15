using System;

namespace FasTnT.Domain.Model.Events
{
    public class TimeZoneOffset
    {
        private int _value;

        public virtual string Representation { get { return ComputeRepresentation(_value); } set { _value = ComputeValue(value); } }
        public virtual int Value { get { return _value; } set { _value = value; } }

        private string ComputeRepresentation(int value)
        {
            var sign = Value >= 0 ? "+" : "-";
            var hours = (Math.Abs(Value) / 60).ToString("D2");
            var minutes = (Math.Abs(Value % 60)).ToString("D2");

            return string.Format("{0}{1}:{2}", sign, hours, minutes);
        }

        private int ComputeValue(string value)
        {
            var sign = (Representation[0] == '-') ? -1 : +1;
            var representation = Representation.TrimStart('-', '+');
            var parts = representation.Split(':');

            return sign * (int.Parse(parts[0]) * 60 + int.Parse(parts[1]));
        }
    }
}
