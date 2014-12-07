using System;

namespace NewsCatcher
{
    public static class DateTimeHelper
    {
        public static DateTime ToDateTime(this int secondsFrom19700101)
        {
            return new DateTime(1970, 1, 1).AddSeconds(secondsFrom19700101);
        }
        public static int ToInt32(this DateTime dateTime)
        {
            return Convert.ToInt32((dateTime - new DateTime(1970, 1, 1)).TotalSeconds);
        }
    }
}