using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus
{
    static class ContentLoader
    {
        public static ContentManager content_manager = null;
        private static Dictionary<String, Texture2D> texture_cache = new Dictionary<string, Texture2D>();
        public static Texture2D get_texture(String path)
        {
            Texture2D texture = null;
            if (texture_cache.ContainsKey(path))
                texture_cache.TryGetValue(path, out texture);
            else
            {
                texture = content_manager.Load<Texture2D>(@path);
                if (texture != null)
                    texture_cache.Add(path, texture);
            }
            return texture;
        }
    }
}
