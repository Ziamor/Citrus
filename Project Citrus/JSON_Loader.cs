using Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Project_Citrus
{
    enum Type
    {
        Entity,
        Image
    };
    static class JSON_Loader
    {
        private static String[] type_dir = new string[] { @"entities", @"images" };
        private static String working_dir = @"C:\Users\Alex\Documents\visual studio 2013\Projects\Project Citrus\Project Citrus\res\";

        private static Dictionary<String, Entity> entity_cache = new Dictionary<string, Entity>();
        private static Dictionary<String, Sprite> sprite_cache = new Dictionary<string, Sprite>();

        public static Entity Get_Entity(String name)
        {
            Entity entity = null;
            if (entity_cache.ContainsKey(name))
            {
                entity_cache.TryGetValue(name, out entity);
            }
            else
            {
                entity = Read<Entity>(name, Type.Entity);
                if (entity != null)
                    entity_cache.Add(name, entity);
            }

            return entity.Clone;
        }

        public static Sprite Get_Sprite(String name)
        {
            Sprite sprite = null;
            sprite = Read<Sprite>(name, Type.Image);
            return sprite;
        }

        private static T Read<T>(String name, Type type)
        {
            T deserialized_object = default(T);
            String json_output = Get_JSON(name, type);
            deserialized_object = JsonConvert.DeserializeObject<T>(json_output);
            return deserialized_object;
        }

        public static void Write_Entity(Entity entity)
        {
            Write<Entity>(entity, entity.Name, Type.Entity);
        }
        private static void Write<T>(T obj, String name, Type type)
        {
            string output = JsonConvert.SerializeObject(obj, Formatting.Indented);
            try
            {
                using (StreamWriter sw = new StreamWriter(Get_Dir(type) + name))
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

        private static String Get_Dir(Type type)
        {

            return working_dir + type_dir[(int)type] + @"\";
        }

        private static String Get_JSON(String name, Type type)
        {
            String json_output = "";
            try
            {
                using (StreamReader sr = new StreamReader(Get_Dir(type) + name))
                {
                    json_output += sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error deserializing entity.");
                Console.WriteLine(e.Message);
            }
            return json_output;
        }
    }
}
