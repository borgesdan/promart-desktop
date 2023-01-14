using System;

namespace Promart.Core
{
    public static class DateTimeExtensions
    {
        public static int GetAge(this DateTime dateTime)
        {
            var now = DateTime.Now;
            var diff = now - dateTime;

            return (int)(diff.TotalDays / 365);
        }
    }
}