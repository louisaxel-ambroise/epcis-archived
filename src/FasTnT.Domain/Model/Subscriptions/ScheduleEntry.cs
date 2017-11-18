using System;
using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Domain.Model.Subscriptions
{
    public class ScheduleEntry
    {
        private readonly List<int> _values = new List<int>();
        private readonly int _minValue, _maxValue;

        public static ScheduleEntry Parse(string expression, int min, int max)
        {
            return new ScheduleEntry(expression, min, max);
        }

        public bool HasValue(int value) => _values.Contains(value);

        public ScheduleEntry()
        {
        }

        private ScheduleEntry(string expression, int min, int max)
        {
            _minValue = min;
            _maxValue = max;

            ParseExpression(expression);
        }

        // Expression can be either "" or a list of elements
        private void ParseExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression)) AddRange(_minValue, _maxValue);
            else foreach (var element in expression.Split(',')) ParseElement(element);
        }

        // An element can be a range ([start-end]) or a number
        private void ParseElement(string element)
        {
            if (element.StartsWith("[") && element.EndsWith("]") && element.Contains("-")) ParseRange(element);
            else if (int.TryParse(element, out int value)) AddValue(value);
            else throw new ArgumentException($"Invalid value: {element}");
        }

        private void ParseRange(string element)
        {
            var rangeParts = element.Substring(1, element.Length - 2).Split('-');

            if (rangeParts.Length != 2) throw new ArgumentException($"Invalid value: {element}");
            if (int.TryParse(rangeParts[0], out int min) && int.TryParse(rangeParts[1], out int max)) AddRange(min, max);
            else throw new ArgumentException($"Invalid value: {element}");
        }

        // Add the values to the list of allowed values or throw exception if values are out of range
        private void AddRange(int minValue, int maxValue)
        {
            if (minValue > maxValue || minValue < _minValue || maxValue > _maxValue) throw new ArgumentException($"Invalid range value: [{minValue}-{maxValue}]");
            _values.AddRange(Enumerable.Range(minValue, (maxValue - minValue + 1)));
        }

        // Add the value to the list of allowed values or throw exception if value is out of range
        private void AddValue(int value)
        {
            if (value < _minValue || value > _maxValue) throw new ArgumentException($"Invalid value: {value}");
            _values.Add(value);
        }
    }
}
