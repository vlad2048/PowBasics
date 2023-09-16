using System.Text.Json.Nodes;
using System.Text.Json;

namespace PowBasics.Json_;

public class Jsoner
{
	private static readonly JsonSerializerOptions defaultJsonOpt = new()
	{
		WriteIndented = true,
	};

	public static readonly Jsoner Default = new(defaultJsonOpt);





	private readonly JsonSerializerOptions jsonOpt;

	public Jsoner(JsonSerializerOptions jsonOpt)
	{
		this.jsonOpt = jsonOpt;
	}

	public string Ser<T>(T obj) => JsonSerializer.Serialize(obj, jsonOpt);

	public T Deser<T>(string str)
	{
		try
		{
			var obj = JsonSerializer.Deserialize<T>(str, jsonOpt);
			return obj switch
			{
				not null => obj,
				null => throw new JsonException($"Error in Jsoner.Deser<{typeof(T).Name}>(...): deserialization returned null")
			};
		}
		catch (JsonException ex)
		{
			throw new JsonException($"Error in Jsoner.Deser<{typeof(T).Name}>(...)", ex);
		}
	}

	public T Deser<T>(JsonNode node)
	{
		try
		{
			var obj = JsonSerializer.Deserialize<T>(node, jsonOpt);
			return obj switch
			{
				not null => obj,
				null => throw new JsonException($"Error in Jsoner.Deser<{typeof(T).Name}>(...): deserialization returned null")
			};
		}
		catch (JsonException ex)
		{
			throw new JsonException($"Error in Jsoner.Deser<{typeof(T).Name}>(...): deserialization error, check inner exception", ex);
		}
	}
}


public static class JsonerExt
{
	public static void Save<T>(this Jsoner jsoner, string file, T obj) => File.WriteAllText(file, jsoner.Ser(obj));

	public static T Load<T>(this Jsoner jsoner, string file) => File.Exists(file) switch
	{
		true => jsoner.Deser<T>(File.ReadAllText(file)),
		false => throw new JsonException($"Error in Jsoner.Load<{typeof(T).Name}>(...): File not found: '{file}'")
	};

	public static T LoadOrCreateDefault<T>(this Jsoner jsoner, string file, T defaultValue)
	{
		T CreateDefault()
		{
			jsoner.Save(file, defaultValue);
			return defaultValue;
		}

		if (!File.Exists(file)) return CreateDefault();

		try
		{
			return jsoner.Load<T>(file);
		}
		catch (JsonException)
		{
			return CreateDefault();
		}
	}
}