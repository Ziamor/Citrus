using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_Citrus.Engine;
using Project_Citrus.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus
{
    class DictionaryComponentConverter : JsonConverter
    {

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                // Load JArray from stream
                JArray jArray = JArray.Load(reader);

                // Create target object based on JObject
                List<Component> target = new List<Component>();

                // Populate the object properties
                serializer.Populate(jArray.CreateReader(), target);
                Dictionary<String, Component> dictionary = new Dictionary<string, Component>();
                foreach (Component comp in target)
                    if (!dictionary.ContainsKey(comp.Name))
                        dictionary.Add(comp.Name, comp);
                return dictionary;
            }
            return new Dictionary<string, Component>();
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Dictionary<String, Component>)
            {
                Dictionary<String, Component> components = (Dictionary<String, Component>)value;
                writer.WriteStartArray();
                foreach (KeyValuePair<String, Component> entry in components)
                {
                    serializer.Serialize(writer, entry.Value);
                }
                writer.WriteEndArray();
            }
            else
            {
                writer.WriteStartObject();
                writer.WriteEndObject();
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Dictionary<string, Component>).IsAssignableFrom(objectType);
        }
    }
}
