using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowBasics.Geom.Serializers;

public class SzSerializer : JsonConverter<Sz>
{
	private record Json(int Width, int Height);
	private static Json ToJson(Sz e) => new(e.Width, e.Height);
	private static Sz FromJson(Json e) => new(e.Width, e.Height);
	
	public override Sz Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		using var doc = JsonDocument.ParseValue(ref reader);
		var json = doc.Deserialize<Json>(options)!;
		return FromJson(json);
	}

	public override void Write(Utf8JsonWriter writer, Sz value, JsonSerializerOptions options)
	{
		var json = ToJson(value);
		JsonSerializer.Serialize(writer, json, options);
	}
}