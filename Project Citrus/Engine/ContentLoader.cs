using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine
{
    static class ContentLoader
    {
        public static ContentManager content_manager = null;
        private static Dictionary<String, Texture2D> texture_cache = new Dictionary<string, Texture2D>();
        public static Texture2D Get_texture(String name)
        {
            Texture2D texture = null;
            if (texture_cache.ContainsKey(name))
                texture_cache.TryGetValue(name, out texture);
            else
            {
                String path = JSON_Loader.Get_ImageInfo(name).Path;
                texture = content_manager.Load<Texture2D>(path);
                if (texture != null)
                    texture_cache.Add(name, texture);
            }
            return texture;
        }
    }
}
