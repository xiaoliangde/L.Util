using System;
using System.Diagnostics;

namespace L.Util
{
    class ServerTimeStamp
    {
        private static Stopwatch _timerWatch = new Stopwatch();
        private static long TimerCounterMsLonin => _timerWatch.ElapsedMilliseconds;
        public static  long GetTimestampOnMs()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000 + TimerCounterMsLonin) / 10000;
        }
        public static long GetTimestampOnS()
        {
            return GetTimestampOnMs() / 1000;
        }
        public static long GetTimestampOnDateTime(string dateTime = null)
        {
            if (dateTime == null) dateTime = DateTime.Now.ToString("d");
            return (DateTime.Parse(dateTime).Ticks - 621355968000000000 + TimerCounterMsLonin) / 10000;
        }
        public static bool CheckTimer(long executeTimeMs,long waitTimeMs)
        {
            if (executeTimeMs <= 0) return false;
            return GetTimestampOnMs() - executeTimeMs - waitTimeMs < 0;
        }
        public static long TimeToLong(int hour, int minutes, int seconds)
        {
            return 3600 * hour * 1000 + 60 * minutes * 1000 + seconds * 1000;
        }
        public static string LongToTime(long milliSeconds)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)milliSeconds);
            var Hour = ts.Hours.ToString();
            if (Hour.Length < 2) 
                Hour = "0" + Hour;
            var Minute = ts.Minutes.ToString();
            if (Minute.Length < 2) 
                Minute = "0" + Minute;
            var Second = ts.Seconds.ToString();
            if (Second.Length < 2)
                Second = "0" + Second;

            return Hour + ":" + Minute + ":" + Second;
        }

        public static long DayMillisNow()
        {
            return (DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second) * 1000;
        }

    }
}
