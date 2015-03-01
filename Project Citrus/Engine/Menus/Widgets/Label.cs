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
        private String text;
        private SpriteFont font;
        public Label(String name, String text) : this(name, text, null, Vector2.Zero, new Vector2(0, 0), null) { }
        public Label(String name, String text, Widget parent) : this(name, text, parent, Vector2.Zero, new Vector2(0, 0), null) { }
        public Label(String name, String text, Widget parent, Vector2 position, Vector2 size, params Widget[] new_widgets)
            : base(name, parent, position, size, Anchor.CENTER, Anchor.CENTER, new_widgets) { this.Text = text; }

        public String Text { get { return text; } set { text = value; } }
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

        public override Vector2 Size
        {
            get
            {
                if (Font != null)
                    return Font.MeasureString(text);
                return new Vector2(1, 1);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, GameTime gameTime)
        {
            Vector2 vec = TransformOriginToTopLeft();
            spriteBatch.DrawString(Font, Text, Real_Position, Color.White);
        }
    }
}
