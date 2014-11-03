using Microsoft.Xna.Framework.Input;
using Project_Citrus.lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine
{
    public class KeyboardManager
    {
        private KeyboardState oldState;
        private KeyboardState newState;

        public KeyboardManager() { oldState = Keyboard.GetState(); }//update the state so it's not null 
        public void UpdateState()
        {
            oldState = newState;
            newState = Keyboard.GetState();
        }
        [LuaFunctionAttr("DebugIsKeyPressed", "Check if the key was pushed.", new String[] { "ID of the Key." })]
        public bool DebugIsKeyPressed(String keyID)
        {
            Keys key;
            switch (keyID)
            {
                case "up":
                    key = Keys.Up;
                    break;
                case "down":
                    key = Keys.Down;
                    break;
                case "left":
                    key = Keys.Left;
                    break;
                case "right":
                    key = Keys.Right;
                    break;
                default:
                    key = Keys.BrowserFavorites;
                    break;
            }
            if (newState.IsKeyDown(key) && oldState.IsKeyDown(key))
                return true;
            return false;
        }
        [LuaFunctionAttr("IsKeyPushed", "Check if the key was pushed.", new String[] { "ID of the Key." })]
        public bool IsKeyPushed(Keys key)
        {
            if (newState.IsKeyDown(key) && !oldState.IsKeyDown(key))
                return true;
            return false;
        }
        [LuaFunctionAttr("IsKeyPressed", "Check if the key is pressed down.", new String[] { "ID of the Key." })]
        public bool IsKeyPressed(Keys key)
        {
            if (newState.IsKeyDown(key) && oldState.IsKeyDown(key))
                return true;
            return false;
        }
        [LuaFunctionAttr("IsKeyReleased", "Check if the key was released.", new String[] { "ID of the Key." })]
        public bool IsKeyReleased(Keys key)
        {
            if (!newState.IsKeyDown(key) && oldState.IsKeyDown(key))
                return true;
            return false;
        }
    }
}
