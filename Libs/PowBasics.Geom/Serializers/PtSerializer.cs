using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowBasics.Geom.Serializers;

public class PtSerializer : JsonConverter<Pt>
{
	private record Json(int X, int Y);
	private static Json ToJson(Pt e) => new(e.X, e.Y);
	private static Pt FromJson(Json e) => new(e.X, e.Y);
	
	public override Pt Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		using var doc = JsonDocument.ParseValue(ref reader);
		var json = doc.Deserialize<Json>(options)!;
		return FromJson(json);
	}

	public override void Write(Utf8JsonWriter writer, Pt value, JsonSerializerOptions options)
	{
		var json = ToJson(value);
		JsonSerializer.Serialize(writer, json, options);
	}
}