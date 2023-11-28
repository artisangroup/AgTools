using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Globalization;

namespace Artisan.Tools.Extensions
{
    /// <summary>
    /// Here are some extensions for processing date-time objects
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// This will return the current date-time object as the number of MS since 1/1/1970
        /// </summary>
        public static ulong AgToUnix(this DateTime i)
        {
            return (ulong)((DateTimeOffset)i).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// returns the date-time objects formatted as local time (24-hr clock) yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static string AgFormatAsLocal(this DateTime i)
        {
            return $"{i.ToLocalTime().ToShortDateString()} {i.ToLocalTime().ToLongTimeString()}".AgReplace("/", "-");
        }
    }

    ///// <summary>
    ///// The DateTimeSerializer class helps process date-time objects
    ///// </summary>
    //public class DateTimeSerializer : JsonConverter<DateTime>
    //{
    //    public override DateTime Read(
    //            ref Utf8JsonReader reader,
    //            Type typeToConvert,
    //            JsonSerializerOptions options)
    //    {
    //        return DateTime.ParseExact(reader.GetString()!, "MM/dd/yyyy", CultureInfo.InvariantCulture);
    //    }

    //    public override void Write(
    //        Utf8JsonWriter writer,
    //        DateTime dateTimeValue,
    //        JsonSerializerOptions options)
    //    {
    //        writer.WriteStringValue(dateTimeValue.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
    //    }
    //}
}

