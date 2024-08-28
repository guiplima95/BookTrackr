using System.Text.Json;
using System.Text.Json.Serialization;

namespace Book.API.Converters;

public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetDateTimeOffset();
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        if (value.Offset == TimeSpan.Zero) // UTC
        {
            writer.WriteStringValue(value.UtcDateTime);
        }
        else
        {
            writer.WriteStringValue(value);
        }
    }
}