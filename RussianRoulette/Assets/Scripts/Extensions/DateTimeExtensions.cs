using System;

namespace TatRat.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToUnixUtcTimestamp(this DateTime dt)
        {
            return (long)(TimeZoneInfo.ConvertTimeToUtc(dt) - DateTime.UnixEpoch).TotalSeconds;
        }

        public static long ToUnixLocalTimestamp(this DateTime dt)
        {
            return (long)(dt - DateTime.UnixEpoch).TotalSeconds;
        }
    }
}