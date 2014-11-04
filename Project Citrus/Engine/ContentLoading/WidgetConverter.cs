using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_Citrus.Engine.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.ContentLoading
{
    class WidgetConverter: JsonConverter
    {
        protected Widget Create(Type objectType, JObject jObject)
        {
            if (jObject["type"] != null)
                if (jObject["type"].ToString() == "button")
                {
                    return new Button();
                }
            return new Widget();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            Widget target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Button)
            {
                Button button = (Button)value;
                writer.WriteStartObject();
                writer.WritePropertyName("type");
                writer.WriteValue("button");
                writer.WritePropertyName("name");
                writer.WriteValue(button.Name);
                writer.WritePropertyName("image_name");
                writer.WriteValue(button.Image_Name);
                writer.WritePropertyName("x");
                writer.WriteValue(button.Y);
                writer.WritePropertyName("y");
                writer.WriteValue(button.X);
                writer.WritePropertyName("width");
                writer.WriteValue(button.Width);
                writer.WritePropertyName("height");
                writer.WriteValue(button.Height);
                writer.WriteEndObject();
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Widget).IsAssignableFrom(objectType);
        }
    }
}   