using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Citrus.Engine.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.Managers
{
    public class MenuManager
    {
        private Stack<Menu> menus;
        public MenuManager() { menus = new Stack<Menu>(); }

        public void Add_Menu(Menu menu)
        {
            menus.Push(menu);
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Stack<Menu> menus_buffer = new Stack<Menu>(); // We need to re-add the menus we pop
            while(menus.Count > 0) // Loop through all our menus and draw until we run out of menus or a menu blocks draw
            {
                Menu poped_menu = menus.Pop();
                menus_buffer.Push(poped_menu);
                poped_menu.Draw(spriteBatch, gameTime);
                if (poped_menu.BlockDraw)
                    break; // Menu blocks draw we need to to exit the loop
            }
            while (menus_buffer.Count > 0) // We need to re-add the poped menus
            {
                Menu poped_menu = menus_buffer.Pop();
                menus.Push(poped_menu);
            }
        }
    }
}
