using System;
using System.Collections.Generic;
using System.Text;

namespace NexStar.Net.NexstarSupport.Models
{
    public class TimeData
    {
        public Int16 Hour { get; set; }
        public Int16 Minutes { get; set; }
        public Int16 Seconds { get; set; }
        public Int16 Month { get; set; }
        public Int16 Day { get; set; }
        public Int16 YearsSince2000 { get; set; }
        public Int16 GMTOffset { get; set; }
        public bool IsDaylightSavings { get; set; }

        public TimeData() { }

        public TimeData(byte[] rawBytes)
        {
            Hour = Convert.ToInt16(rawBytes[0]);
            Minutes = Convert.ToInt16(rawBytes[1]);
            Seconds = Convert.ToInt16(rawBytes[2]);
            Month = Convert.ToInt16(rawBytes[3]);
            Day = Convert.ToInt16(rawBytes[4]);
            YearsSince2000 = Convert.ToInt16(rawBytes[5]);
            GMTOffset = Convert.ToInt16(rawBytes[6]);
            IsDaylightSavings = Convert.ToBoolean(rawBytes[7]);
        }

        public DateTimeOffset ToDateTimeOffset()
        {
            var datetime = new DateTime(YearsSince2000 + 2000, Month, Day, Hour, Minutes, Seconds);
            return new DateTimeOffset(datetime, new TimeSpan(0, GMTOffset, 0, 0));
        }

        public static TimeData FromDateTimeOffset(DateTimeOffset offset)
        {
            return new TimeData()
            {
                Seconds = (Int16)offset.Second,
                Minutes = (Int16)offset.Minute,
                Hour = (Int16)offset.Hour,
                Day = (Int16)offset.Day,
                Month = (Int16)offset.Month,
                YearsSince2000 = (Int16)(offset.Year - 2000),
                GMTOffset = (Int16)offset.Offset.Hours,
                IsDaylightSavings = offset.Date.IsDaylightSavingTime()
            };
        }
    }
}
