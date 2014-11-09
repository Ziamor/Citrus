using Project_Citrus.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Citrus.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.EntitySystems
{
    class RenderSystem : EntitySystem
    {
        public override string[] Required_Components
        {
            get { return new String[] { "position", "image" }; }
        }

        public override bool Initialize(params object[] param)
        {
            return true;
        }
        //params[0] = Spritebatch, params[1] = GameTime
        public override void Execute(params object[] param)
        {
            SpriteBatch spriteBatch = (SpriteBatch)param[0];
            GameTime gameTime = (GameTime)param[1];
            foreach (Entity entity in entities)
            {
                Position position = (Position)entity.Get_Component("position");
                Image image = (Image)entity.Get_Component("image");
                //Component comp = new Position();
                if (position != null && image != null)
                {
                    spriteBatch.Draw(image.Image_Info.Tex, new Vector2(position.X, position.Y), Color.White);
                }
            }
        }
    }
}
