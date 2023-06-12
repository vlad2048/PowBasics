using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowBasics.Geom.Serializers;

public sealed class VecPtSerializer : JsonConverter<VecPt>
{
	private record Json(double X, double Y);
	private static Json ToJson(VecPt e) => new(e.X, e.Y);
	private static VecPt FromJson(Json e) => new(e.X, e.Y);
	
	public override VecPt Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		using var doc = JsonDocument.ParseValue(ref reader);
		var json = doc.Deserialize<Json>(options)!;
		return FromJson(json);
	}

	public override void Write(Utf8JsonWriter writer, VecPt value, JsonSerializerOptions options)
	{
		var json = ToJson(value);
		JsonSerializer.Serialize(writer, json, options);
	}
}