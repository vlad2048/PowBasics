using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowBasics.Geom.Serializers;

public class RSerializer : JsonConverter<R>
{
	private record Json(int X, int Y, int Width, int Height);
	private static Json ToJson(R e) => new(e.X, e.Y, e.Width, e.Height);
	private static R FromJson(Json e) => new(e.X, e.Y, e.Width, e.Height);
	
	public override R Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		using var doc = JsonDocument.ParseValue(ref reader);
		var json = doc.Deserialize<Json>(options)!;
		return FromJson(json);
	}

	public override void Write(Utf8JsonWriter writer, R value, JsonSerializerOptions options)
	{
		var json = ToJson(value);
		JsonSerializer.Serialize(writer, json, options);
	}
}