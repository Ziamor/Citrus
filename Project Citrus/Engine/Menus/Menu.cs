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
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class Menu
    {
        [JsonProperty(Required = Required.Always)]
        private String name;
        [JsonProperty(Required = Required.Always)]
        private Boolean blockUpdate;
        [JsonProperty(Required = Required.Always)]
        private Boolean blockDraw;
        [JsonProperty(Required = Required.Always)]
        private Boolean blockInput;
        [JsonProperty(), JsonConverter(typeof(WidgetListConverter))]
        private List<Widget> widgets;
        public Menu() { widgets = new List<Widget>(); }

        public Menu(String name, Boolean blockUpdate, Boolean blockDraw, Boolean blockInput, params Widget[] new_widgets)
            : base()
        {
            this.name = name;
            this.blockUpdate = blockUpdate;
            this.blockDraw = blockDraw;
            this.blockInput = blockInput;
            widgets = new List<Widget>();
            foreach (Widget widget in new_widgets)
                widgets.Add(widget);
        }

        public String Name { get { return name; } }
        public Boolean BlockUpdate { get { return blockUpdate; } }
        public Boolean BlockDraw { get { return blockDraw; } }
        public Boolean BlockInput { get { return blockInput; } }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Widget widget in widgets)
            {
                widget.Draw(spriteBatch, gameTime);
            }
        }
    }
}
