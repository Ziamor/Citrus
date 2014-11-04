using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Project_Citrus.Engine.ContentLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.Menus
{
    [JsonObject(MemberSerialization.OptIn), JsonConverter(typeof(WidgetConverter))]
    public class Widget
    {
        [JsonProperty()]
        protected String name;
        [JsonProperty()]
        protected int x;
        [JsonProperty()]
        protected int y;
        [JsonProperty()]
        protected int width;
        [JsonProperty()]
        protected int height;
        [JsonProperty()]
        protected String image_name;

        public String Name { get { return name; } }
        public String Image_Name { get { return image_name; } }
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) { }
    }
}
