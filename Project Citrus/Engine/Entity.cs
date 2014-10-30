using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using Project_Citrus;

namespace Engine
{
    [JsonObject(MemberSerialization.OptIn)]
    class Entity
    {
        private static List<Entity> entities = new List<Entity>(); // All entities in a list
        private static Dictionary<String, List<Entity>> tag_entities = new Dictionary<String, List<Entity>>(); // List keeps track of entity tags

        [JsonProperty(Required = Required.Always)]
        private String name = "";
        [JsonProperty(Required = Required.Always)]
        private Entity_Type entity_type = null;
        [JsonProperty(Required = Required.Always)]
        private String[] tags; // A tag is used to find objects quickly        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private String texture = null;
        private Sprite sprite = null;

        public Entity()
        {
        }

        public Entity(String name, Entity_Type entity_type, String[] tags)
        {
            this.name = name;
            this.entity_type = entity_type;
            this.tags = tags;
        }

        public Entity(String name, Entity_Type entity_type, String texture, String[] tags)
            : this(name, entity_type, tags)
        {
            this.texture = texture;
        }

        public String Name
        {
            get { return name; }
        }

        public Entity_Type Entity_Type
        {
            get { return entity_type; }
        }

        public String[] Tags
        {
            get { return tags; }
        }

        public Sprite Sprite
        {
            get
            {
                if (sprite == null && texture != null)
                {
                    // We haven't loaded the spirte yet, so we need to do that
                    sprite = JSON_Loader.Get_Sprite(texture);// new Sprite("worker", @"images\worker");
                }

                return sprite;
            }
        }

        public static List<Entity> Entities
        {
            get { return entities; }
        }

        public static void Add_Entity(Entity entity)
        {
            if (entity != null)
            {
                entities.Add(entity);
                if (tag_entities.Count == 0)// Tags have yet to be intialized
                    Initialize_Tags();
                foreach (String tag in entity.Tags)
                {
                    if (tag_entities.ContainsKey(tag))
                    {// If the tag is  valid, we need to get and add it to the correct list 
                        List<Entity> tag_list;
                        tag_entities.TryGetValue(tag, out tag_list);
                        if (tag_list != null) // Make sure we got a valid list
                        {
                            tag_list.Add(entity);
                            continue; // Entity added with tag, continue loop
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("Error adding entity with tag: " + tag); // We found an invalid tag
                }
            }
        }

        public static void Data_Dump()
        {
            System.Diagnostics.Debug.WriteLine("Entities:");
            foreach (Entity ent in entities)
                System.Diagnostics.Debug.WriteLine(ent.name);
            foreach (KeyValuePair<String, List<Entity>> list in tag_entities)
            {
                System.Diagnostics.Debug.WriteLine("\n" + list.Key + ":");
                foreach (Entity ent in list.Value)
                    System.Diagnostics.Debug.WriteLine(ent.name);
            }
        }
        private static void Initialize_Tags()
        {
            tag_entities.Add("drawable", new List<Entity>());
            tag_entities.Add("solid", new List<Entity>());
            tag_entities.Add("unit", new List<Entity>());
            tag_entities.Add("environment", new List<Entity>());
        }

        public Entity Clone
        {
            get
            {
                Entity copy = new Entity();
                copy.name = this.name;
                copy.entity_type = this.entity_type;
                copy.tags = this.tags;
                copy.texture = this.texture;
                return copy;
            }
        }
    }
}
