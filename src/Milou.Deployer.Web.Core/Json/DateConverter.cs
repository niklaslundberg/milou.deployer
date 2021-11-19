using System;
using Arbor.AppModel.Time;
using Newtonsoft.Json;

namespace Milou.Deployer.Web.Core.Json
{
    public class DateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(Date);

        public override object? ReadJson(JsonReader? reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer)
        {
            if (reader?.Value is null)
            {
                return null;
            }

            if (!DateTime.TryParse(reader.Value?.ToString(), out var dateTime))
            {
                return null;
            }

            return new Date(dateTime);
        }

        public override void WriteJson(JsonWriter? writer, object? value, JsonSerializer serializer)
        {
            if (value is null)
            {
                return;
            }

            writer?.WriteValue(((Date)value).ToString());
        }
    }
}