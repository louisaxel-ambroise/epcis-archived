using System;

namespace FasTnT.Domain.Model.Subscriptions
{
    public class SubscriptionSchedule
    {
        private string _seconds, _minutes, _hours, _dayOfMonth, _month, _dayOfWeek;

        private ScheduleEntry _secondsSchedule = new ScheduleEntry();
        private ScheduleEntry _minutesSchedule = new ScheduleEntry();
        private ScheduleEntry _hoursSchedule = new ScheduleEntry();
        private ScheduleEntry _dayOfMonthSchedule = new ScheduleEntry();
        private ScheduleEntry _monthSchedule = new ScheduleEntry();
        private ScheduleEntry _dayOfWeekSchedule = new ScheduleEntry();

        public virtual Guid Id { get; set; }
        public virtual string Seconds { get { return _seconds; } set { _seconds = value; _secondsSchedule = ScheduleEntry.Parse(value, 0, 59); } }
        public virtual string Minutes { get { return _minutes; } set { _minutes = value; _minutesSchedule = ScheduleEntry.Parse(value, 0, 59); } }
        public virtual string Hours { get { return _hours; } set { _hours = value; _hoursSchedule = ScheduleEntry.Parse(value, 0, 23); } }
        public virtual string DayOfMonth { get { return _dayOfMonth; } set { _dayOfMonth = value; _dayOfMonthSchedule = ScheduleEntry.Parse(value, 1, 31); } }
        public virtual string Month { get { return _month; } set { _month = value; _monthSchedule = ScheduleEntry.Parse(value, 1, 12); } }
        public virtual string DayOfWeek { get { return _dayOfWeek; } set { _dayOfWeek = value; _dayOfWeekSchedule = ScheduleEntry.Parse(value, 1, 7); } }

        public SubscriptionSchedule()
        {
            Seconds = "";
            Minutes = "";
            Hours = "";
            DayOfMonth = "";
            Month = "";
            DayOfWeek = "";
        }

        public virtual DateTime GetNextOccurence(DateTime startDate)
        {
            var tentative = startDate.AddSeconds(1); // Parse from the next second

            while (!_secondsSchedule.HasValue(tentative.Second)) tentative = tentative.AddSeconds(1);
            while (!_minutesSchedule.HasValue(tentative.Minute)) tentative = tentative.AddMinutes(1);
            while (!_hoursSchedule.HasValue(tentative.Hour)) tentative = tentative.AddHours(1);
            while (!_dayOfMonthSchedule.HasValue(tentative.Day)) tentative = tentative.AddDays(1);
            while (!_monthSchedule.HasValue(tentative.Month)) tentative = tentative.AddMonths(1);

            if (!_dayOfWeekSchedule.HasValue(1 + (int)tentative.DayOfWeek)) return GetNextOccurence(new DateTime(tentative.Year, tentative.Month, tentative.Day, 23, 59, 59));
            return tentative;
        }
    }
}
