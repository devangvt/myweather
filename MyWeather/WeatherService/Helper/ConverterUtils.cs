using System;

namespace DevangsWeather.Service.Helper
{
    public static class ConverterUtils
    {
        public static DateTime ConvertUnixTimeStampToDateTime(long timeStamp)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = epoch.AddSeconds(timeStamp);
            return date;
        }
    }
}
