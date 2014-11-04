using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Citrus.Engine.ContentLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.Menus
{
    class Button : Widget
    {
        public Button() { }
        public Button(String name) { this.name = name; this.image_name = "button_green"; }
        private Texture2D tex = null;
        private SpriteFont font = null;
        private Texture2D Tex
        {
            get
            {
                if (tex != null)
                    return tex;
                tex = ContentLoading.ContentLoader.Get_texture(this.image_name);
                return tex;
            }
        }

        private SpriteFont Font
        {
            get
            {
                if (font != null)
                    return font;
                font = ContentLoading.ContentLoader.Get_Font("fonts\\Main_Menu");
                return font;
            }
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Vector2 string_size = Font.MeasureString("Button");
            float height_percent = (10f * 2f + string_size.Y) / 168f;
            float width_percent = (25f * 2f + string_size.X) / 496;
            spriteBatch.Draw(Tex, new Vector2(this.X, this.Y), null, Color.White, 0, Vector2.Zero, new Vector2(width_percent, height_percent), SpriteEffects.None, 0);
            spriteBatch.DrawString(Font, "Button", new Vector2(this.X, this.Y), Color.White);
        }
    }
}
