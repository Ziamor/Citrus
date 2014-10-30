using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    static class Entity_Factory
    {
        /*public static String working_dir = @"C:\Users\Alex\Documents\visual studio 2013\Projects\Project Citrus\Project Citrus\res\entities\";

        private static Dictionary<String, Entity> cache = new Dictionary<string, Entity>();
        public static Entity get(String key)
        {
            Entity entity = null;
            if (cache.ContainsKey(key))
            {
                Entity cached_entity = null;
                cache.TryGetValue(key, out cached_entity);
                entity = Entity.Clone(cached_entity);
            }
            else
            {
                entity = Read(key);
                if (entity != null)
                    cache.Add(key, entity);
            }
            return entity;
        }

        private static Entity Read(String key)
        {
            Entity deserialized_entity = null;
            String json_output = "";
            try
            {
                using (StreamReader sr = new StreamReader(working_dir + key))
                {
                    json_output += sr.ReadToEnd();
                }
                deserialized_entity = JsonConvert.DeserializeObject<Entity>(json_output);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error deserializing entity.");
                Console.WriteLine(e.Message);
            }
            return deserialized_entity;
        }


        public static void Write(Entity entity)
        {
            string output = JsonConvert.SerializeObject(entity, Formatting.Indented);
            try
            {
                using (StreamWriter sw = new StreamWriter(working_dir + entity.Name))
                {
                    sw.Write(output);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not write to file:");
                Console.WriteLine(e.Message);
            }
        }

        public static Entity read2(String key)
        {            
            Entity entity = null;
            String json_output = "";
            try
            {
                using (StreamReader sr = new StreamReader(working_dir + key))
                {
                    json_output += sr.ReadToEnd();
                }

                JObject jObject = JObject.Parse(json_output);

                if (jObject["entity"] != null)
                {
                    // The object is in fact an entity

                    String name = null;
                    String tags;
                    if (jObject["entity"]["name"] != null)
                        name = jObject["entity"]["name"].ToString();
                    else
                        return null;

                    if (jObject["entity"]["tags"] != null)
                        tags = jObject["entity"]["tags"].ToString();
                    else
                        return null;
                    Debug.WriteLine(name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not read file.");
                Console.WriteLine(e.Message);
            }
            return entity;
        }*/
    }
}
