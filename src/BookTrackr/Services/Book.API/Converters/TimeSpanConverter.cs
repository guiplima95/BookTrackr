using System.Text.Json;
using System.Text.Json.Serialization;

namespace Book.API.Converters;

/// <summary>
/// In the dotnet 6 version, the default value supported for TimeSpan is "00:00:00" format,
/// but it does not support the format "00:00", making it necessary to create a converter.
/// Doc -> https://learn.microsoft.com/en-us/dotnet/core/compatibility/serialization/6.0/timespan-serialization-format
/// </summary>
public class TimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeSpan.Parse(reader.GetString() ?? string.Empty, formatProvider: null);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}