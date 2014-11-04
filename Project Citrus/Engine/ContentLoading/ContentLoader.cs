using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.ContentLoading
{
    static class ContentLoader
    {
        public static ContentManager content_manager = null;
        private static Dictionary<String, Texture2D> texture_cache = new Dictionary<string, Texture2D>();
        private static Dictionary<String, SpriteFont> font_cache = new Dictionary<string, SpriteFont>();
        public static Texture2D Get_texture(String name)
        {
            Texture2D texture = null;
            if (texture_cache.ContainsKey(name))
                texture_cache.TryGetValue(name, out texture);
            else
            {
                ImageInfo imageInfo = JSON_Loader.Get_ImageInfo(name);
                String path = imageInfo.Path;
                texture = content_manager.Load<Texture2D>(path);
                imageInfo.Width = texture.Width;
                imageInfo.Height = texture.Height;
                if (texture != null)
                    texture_cache.Add(name, texture);
            }
            return texture;
        }
        public static SpriteFont Get_Font(String name)
        {
            SpriteFont font = null;
             if (font_cache.ContainsKey(name))
                font_cache.TryGetValue(name, out font);
            else
             {
                 font = content_manager.Load<SpriteFont>(name);
                 if (font != null)
                     font_cache.Add(name, font);
             }
             return font;
        }
    }
}
