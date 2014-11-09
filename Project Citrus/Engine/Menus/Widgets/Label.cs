using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Citrus.Engine.Menus.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.Menus.Widgets
{
    public class Label : Widget
    {
        private String Text;
        private SpriteFont font;
        public Label() : base() { this.Text = ""; }
        public Label(String name, String text) : base(name) { this.Text = text; }
        public Label(String name, String text, Widget parent) : base(name, parent) { this.Text = text; }
        public Label(String name, String text, Vector2 position, Vector2 size) : base(name, position, size) { this.Text = text; }
        public SpriteFont Font
        {
            get
            {
                if (font != null)
                    return font;
                font = ContentLoading.ContentLoader.Get_Font("fonts\\Main_Menu");
                return font;
            }
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(Font, Text, Real_Position, Color.White);
        }
    }
}
