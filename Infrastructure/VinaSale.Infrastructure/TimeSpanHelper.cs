using System;

namespace VinaSale.Infrastructure
{
    public class TimeSpanHelper
    {
        public static TimeSpan Parse(string timeSpanString)
        {
            try
            {
                var timeSpanParts = timeSpanString.Split(':');
                switch (timeSpanParts.Length)
                {
                    case 3:
                        return new TimeSpan(
                            int.Parse(timeSpanParts[0]),
                            int.Parse(timeSpanParts[1]),
                            int.Parse(timeSpanParts[2]));
                    case 4:
                        return new TimeSpan(
                            int.Parse(timeSpanParts[0]),
                            int.Parse(timeSpanParts[1]),
                            int.Parse(timeSpanParts[2]),
                            int.Parse(timeSpanParts[3]));
                    default:
                        throw new ArgumentException(string.Format("Cannot parse the value {0} to timespan", timeSpanString));
                }
            }
            catch
            {
                throw new ArgumentException(string.Format("Cannot parse the value {0} to timespan", timeSpanString));
            }
        }
    }
}
