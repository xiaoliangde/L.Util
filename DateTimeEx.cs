using System;

namespace L.Util
{
    public static class DateTimeEx
    {
        public static DateTime ToLocalDateTime(this long timestamp, bool isMs = false)
        {
            //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            //if (isMs)
            //{
            //    return startTime.AddTicks(timestamp * 10000);
            //}
            //else
            //{
            //    return startTime.AddTicks(timestamp * 10000000);
            //}
            var dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
            var toNow = new TimeSpan(isMs?timestamp* 10000000: timestamp * 10000);
            return dtStart.Add(toNow);

        }

        /// <summary>
        /// DateTime 为非Utc
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="isMs"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime dateTime , bool isMs = true)
        {
            return (dateTime.ToUniversalTime().Ticks - 621355968000000000L) / (isMs ? 10000L : 10000000L);
        }
    }
}
