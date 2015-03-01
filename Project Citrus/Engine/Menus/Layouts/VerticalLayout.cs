using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Citrus.Engine.Menus.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.Menus.Layouts
{
    class VerticalLayout : Widget
    {        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (widgets != null)
                foreach (Widget widget in widgets)
                {
                    widget.Draw(spriteBatch, gameTime);
                }
        }
    }
}
