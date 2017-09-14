using System;

namespace FasTnT.Domain.Model.Events
{
    public class TimeZoneOffset
    {
        public string Representation { get; private set; }
        public int Value { get; private set; }

        public TimeZoneOffset(string representation)
        {
            Representation = representation;

            ComputeValue();
        }

        public TimeZoneOffset(int value)
        {
            Value = value;

            ComputeRepresentation();
        }

        private void ComputeRepresentation()
        {
            var sign = Value >= 0 ? "+" : "-";
            var hours = (Math.Abs(Value) / 60).ToString("D2");
            var minutes = (Math.Abs(Value % 60)).ToString("D2");

            Representation = string.Format("{0}{1}:{2}", sign, hours, minutes);
        }

        private void ComputeValue()
        {
            var sign = (Representation[0] == '-') ? -1 : +1;
            var representation = Representation.TrimStart('-', '+');
            var parts = representation.Split(':');

            Value = sign * (int.Parse(parts[0]) * 60 + int.Parse(parts[1]));
        }
    }
}
