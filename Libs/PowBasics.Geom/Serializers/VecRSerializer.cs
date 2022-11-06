using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowBasics.Geom.Serializers;

public class VecRSerializer : JsonConverter<VecR>
{
	private record Json(VecPt Min, VecPt Max);
	private static Json ToJson(VecR e) => new(e.Min, e.Max);
	private static VecR FromJson(Json e) => new(e.Min, e.Max);
	
	public override VecR Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		using var doc = JsonDocument.ParseValue(ref reader);
		var json = doc.Deserialize<Json>(options)!;
		return FromJson(json);
	}

	public override void Write(Utf8JsonWriter writer, VecR value, JsonSerializerOptions options)
	{
		var json = ToJson(value);
		JsonSerializer.Serialize(writer, json, options);
	}
}