using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_Citrus.Engine.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.ContentLoading
{
    class WidgetListConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                // Load JArray from stream
                JArray jArray = JArray.Load(reader);

                // Create target object based on JObject
                List<Widget> target = new List<Widget>();

                // Populate the object properties
                serializer.Populate(jArray.CreateReader(), target);
                return target;
            }
            return new List<Widget>();
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is List<Widget>)
            {
                List<Widget> widgets = (List<Widget>)value;
                writer.WriteStartArray();
                foreach (Widget widget in widgets)
                {
                    serializer.Serialize(writer, widget);
                }
                writer.WriteEndArray();
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Widget).IsAssignableFrom(objectType);
        }
    }
}
