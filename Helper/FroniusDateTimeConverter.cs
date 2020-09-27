
namespace FroniusReader.Helper
{
    using System;

    public static class FroniusDateTimeConverter
    {
        public static string ToFroniusDateTimeString(DateTime dateTime)
        {
            string s = dateTime.ToLocalTime().ToString("yyyy-MM-dd\"T\"HH:mm:sszzz");
            return s;
        }
    }
}
