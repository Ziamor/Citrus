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
using Project_Citrus.Engine.ContentLoading;
using Project_Citrus.Engine.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project_Citrus.Engine.lua;

namespace Project_Citrus.Engine
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class Entity
    {
        [JsonProperty(Required = Required.Always)]
        private String name = "";
        [JsonProperty(Required = Required.Always), JsonConverter(typeof(DictionaryComponentConverter))]
        private Dictionary<String, Component> components = new Dictionary<String, Component>();

        public Entity() { }

        public Entity(String name, params Component[] components)
        {
            this.name = name;
            foreach (Component comp in components)
            {
                if (!this.components.ContainsKey(comp.Name))
                    this.components.Add(comp.Name, comp);
            }
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
                copy.components = new Dictionary<string, Component>();
                foreach (KeyValuePair<String, Component> entry in this.components)
                {
                    String key = entry.Key.ToString();
                    Component comp = entry.Value;
                    copy.components.Add(key, comp);
                }
                return copy;
            }
        }

        [LuaFunctionAttr("Get_Component", "Get a component of an entity, returns null is the entity does not have the component.", new String[] { "ID of the component type." })]
        public Component Get_Component(String name)
        {
            Component comp = null;
            if (components.ContainsKey(name))
                components.TryGetValue(name, out comp);
            return comp;
        }

        [LuaFunctionAttr("Has_Component", "Check if an entity has a component.", new String[] { "ID of the component type." })]
        public Boolean Has_Component(String name)
        {
            if (components.ContainsKey(name))
                return true;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Position position = (Position)Get_Component("position");
            Image image = (Image)Get_Component("image");
            //Component comp = new Position();
            if (position != null && image != null)
            {
                spriteBatch.Draw(image.Tex, new Vector2(position.X, position.Y), Color.White);
            }
        }

        [LuaFunctionAttr("Test", "Test, please ignore.")]
        public void Test()
        {
            System.Diagnostics.Debug.WriteLine("123 test");
        }
    }
}
