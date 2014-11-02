using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_Citrus.Engine;
using Project_Citrus.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine
{
    class ComponentConverter : JsonConverter
    {
        protected Component Create(Type objectType, JObject jObject)
        {
            if (jObject["name"] != null)
                if (jObject["name"].ToString() == "position")
                {
                    return new Position();
                }
                else if (jObject["name"].ToString() == "health")
                {
                    return new Health();
                }
                else if (jObject["name"].ToString() == "image")
                {
                    return new Image();
                }

            return new Component();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            Component target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Position)
            {
                Position position = (Position)value;
                writer.WriteStartObject();
                writer.WritePropertyName("name");
                writer.WriteValue(position.Name);
                writer.WritePropertyName("x");
                writer.WriteValue(position.x);
                writer.WritePropertyName("y");
                writer.WriteValue(position.y);
                writer.WriteEndObject();
            }
            else if (value is Health)
            {
                Health health = (Health)value;
                writer.WriteStartObject();
                writer.WritePropertyName("name");
                writer.WriteValue(health.Name);
                writer.WritePropertyName("HP");
                writer.WriteValue(health.HP);
                writer.WriteEndObject();
            }
            else if (value is Image)
            {
                Image image = (Image)value;
                writer.WriteStartObject();
                writer.WritePropertyName("name");
                writer.WriteValue(image.Name);
                writer.WritePropertyName("image_name");
                writer.WriteValue(image.image_name);
                writer.WriteEndObject();
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Component).IsAssignableFrom(objectType);
        }
    }
}
