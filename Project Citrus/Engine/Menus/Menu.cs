using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Project_Citrus.Engine.ContentLoading;
using Project_Citrus.Engine.Menus.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.Menus
{
    public class Menu
    {
        private String name;
        private Boolean blockUpdate;
        private Boolean blockDraw;
        private Boolean blockInput;
        private Panel menu_panel;
        public Menu() { menu_panel = new Panel(); }//widgets = new List<Widget>(); }

        public Menu(String name, Vector2 size, Boolean blockUpdate, Boolean blockDraw, Boolean blockInput, params Widget[] new_widgets)
            : base()
        {
            this.name = name;
            this.blockUpdate = blockUpdate;
            this.blockDraw = blockDraw;
            this.blockInput = blockInput;
            menu_panel = new Panel("pnl_root",Vector2.Zero, size, new_widgets);
        }

        public String Name { get { return name; } }
        public Boolean BlockUpdate { get { return blockUpdate; } }
        public Boolean BlockDraw { get { return blockDraw; } }
        public Boolean BlockInput { get { return blockInput; } }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            menu_panel.Draw(spriteBatch, gameTime);
        }
    }
}
