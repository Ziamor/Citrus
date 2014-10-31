using Engine;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Project_Citrus
{
    enum Resource_Type
    {
        Entity,
        ImageInfo
    };
    static class JSON_Loader
    {
        private static String[] type_dir = new string[] { @"entities", @"images" };
        private static String working_dir = @"C:\Users\Alex\Documents\visual studio 2013\Projects\Project Citrus\Project Citrus\res\";

        private static Dictionary<String, Entity> entity_cache = new Dictionary<string, Entity>();
        private static Dictionary<String, ImageInfo> imageInfo_cache = new Dictionary<string, ImageInfo>();

        public static Entity Get_Entity(String name)
        {
            Entity entity = null;
            entity = Get<Entity>(name, Resource_Type.Entity, entity_cache);
            if (entity != null)
                return DeepCopy.DeepClone<Entity>(entity.Clone);
            else return null;
        }

        public static ImageInfo Get_ImageInfo(String name)
        {
            ImageInfo imageInfo = null;
            imageInfo = Get<ImageInfo>(name, Resource_Type.ImageInfo, imageInfo_cache);
            return imageInfo;
        }

        private static T Get<T>(String name, Resource_Type type, Dictionary<String, T> cache)
        {
            T obj = default(T);
            if (cache.ContainsKey(name))
                cache.TryGetValue(name, out obj);
            else
            {
                obj = Read<T>(name, type);
                if (obj != null)
                    cache.Add(name, obj);
            }
            return obj;
        }

        private static T Read<T>(String name, Resource_Type type)
        {
            T deserialized_object = default(T);
            String json_output = Get_JSON(name, type);
            deserialized_object = JsonConvert.DeserializeObject<T>(json_output);
            return deserialized_object;
        }

        public static void Write_Entity(Entity entity)
        {
            Write<Entity>(entity, entity.Name, Resource_Type.Entity);
        }
        private static void Write<T>(T obj, String name, Resource_Type type)
        {
            //JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
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

        private static String Get_Dir(Resource_Type type)
        {

            return working_dir + type_dir[(int)type] + @"\";
        }

        private static String Get_JSON(String name, Resource_Type type)
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
