
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class PolymorphicJsonCreationConverter : JsonConverter
{
    public override bool CanWrite { get; } = false;

    public override bool CanRead { get; } = true;

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
    }

    private object Create(Type objectType, JObject jObject)
    {
        var types = Assembly.GetExecutingAssembly()
               .GetTypes().Where(t => t.IsSubclassOf(objectType)).ToList();
        types.Add(objectType);

        var allKeys = new List<string>();
        foreach (var o in jObject)
        {
            allKeys.Add(o.Key.ToLower());
        }

        var left = int.MaxValue;
        var bestType = objectType;
        foreach (var item in types)
        {
            var allProps = item.GetProperties().Select(a => a.Name.ToLower());
            var count = allKeys.Except(allProps).Count();
            if (count < left)
            {
                bestType = item;
                left = count;
            }
        }

        return Activator.CreateInstance(bestType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        var jObject = JObject.Load(reader);

        var target = Create(objectType, jObject);

        serializer.Populate(jObject.CreateReader(), target);

        return target;
    }

    public override bool CanConvert(Type objectType)
    {
        return true;
    }
}