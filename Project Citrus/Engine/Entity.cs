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
using Project_Citrus.Engine;
using Project_Citrus.Engine.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Engine
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    class Entity
    {
        private static List<Entity> entities = new List<Entity>(); // All entities in a list
        private static Dictionary<String, List<Entity>> tag_entities = new Dictionary<String, List<Entity>>(); // List keeps track of entity tags

        [JsonProperty(Required = Required.Always)]
        private String name = "";       
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private String texture = null;
        [JsonProperty(Required = Required.Always), JsonConverter(typeof(DictionaryComponentConverter))]
        private Dictionary<String, Component> components = new Dictionary<String, Component>();

        public Entity()
        {
        }

        public Entity(String name, params Component[] components)
        {
            this.name = name;
            foreach (Component comp in components)
            {
                if (!this.components.ContainsKey(comp.Name))
                    this.components.Add(comp.Name, comp);
            }
        }

        public Entity(String name, String texture, params Component[] components)
            : this(name, components)
        {
           // this.texture = texture;
        }

        public String Name
        {
            get { return name; }
        }

        public Entity Clone
        {
            get
            {
                Entity copy = new Entity();
                copy.name = this.name;
                //copy.entity_type = this.entity_type;
                copy.texture = this.texture;
                copy.components = new Dictionary<string, Component>();
                foreach (KeyValuePair<String, Component> entry in this.components){
                    String key = entry.Key.ToString();
                    Component comp = entry.Value;
                    copy.components.Add(key, comp);
                }
                return copy;
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

        public Component Get_Component(String name)
        {
            Component comp = null;
            if (components.ContainsKey(name))
                components.TryGetValue(name, out comp);
            return comp;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Position position = (Position)Get_Component("position");
            Image image = (Image)Get_Component("image");
            //Component comp = new Position();
            if (position != null && image != null)
            {
                spriteBatch.Draw(image.Tex, new Vector2(position.x, position.y), Color.White);
            }
        }
    }
}
