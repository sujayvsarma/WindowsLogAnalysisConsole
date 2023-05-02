using System;
using System.Collections.Generic;

namespace LogAnalysisConsole
{
    internal static class Timezones
    {
        public static List<TimeZoneInfo> AvailableTimeZones;
        public static TimeZoneInfo LocalTimeZone;

        static Timezones()
        {
            AvailableTimeZones = new List<TimeZoneInfo>();
            IReadOnlyCollection<TimeZoneInfo> systemTzi = TimeZoneInfo.GetSystemTimeZones();
            foreach (TimeZoneInfo tz in systemTzi)
            {
                AvailableTimeZones.Add(tz);
            }

            LocalTimeZone = TimeZoneInfo.Local;
        }
    }
}
