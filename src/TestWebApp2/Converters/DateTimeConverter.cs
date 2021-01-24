using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestWebApp2.Converters
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString(), CultureInfo.GetCultureInfoByIetfLanguageTag("ru-RU"));
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss"));
        }
    }
}
