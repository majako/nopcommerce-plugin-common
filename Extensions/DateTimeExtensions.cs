using System;

namespace Majako.Plugin.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static int ToUnixTimestamp(this DateTime dateTime)
        {
            return (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
