using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _2x2_MsAgentService.Schedules
{

    public class WeeklyRegularity : IRegularity
    {

        public bool IsRepeatable() => true;

        private List<DayOfWeek> weekDays = new List<DayOfWeek>();

        private TimeSpan _time;

        public void SetTime(int hours, int minutes)
        {
            _time = new TimeSpan(hours, minutes, 0);
        }

        public void AddWeekDay(DayOfWeek day)
        {
            if (!weekDays.Contains(day)) weekDays.Add(day);
        }


        public int GetDelayToNextExecution()
        {
            Calendar c = new GregorianCalendar();
            var now = DateTime.Now;
            DateTime dateExec;


            DayOfWeek nowWeekDay = c.GetDayOfWeek(now);
            var delays = new List<double>();
            weekDays.ForEach(day =>
            {
                dateExec = day >= nowWeekDay ? now.AddDays(day - nowWeekDay) : now.AddDays(day + 7 - nowWeekDay);
                dateExec = dateExec.Date + _time;
                delays.Add((now - dateExec).TotalMilliseconds);
            });
            return (int)delays.Min();
        }
    }
}