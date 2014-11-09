using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Project_Citrus.Engine.ContentLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.ContentLoading
{
    // A package that stores Image information
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageInfo
    {
        [JsonProperty(Required = Required.Always)]
        private String name = null;
        [JsonProperty(Required = Required.Always)]
        private String path = null;
        private Texture2D tex = null;
        public ImageInfo() { }
        public String Name { get { return name; } }
        public String Path { get { return path; } }

        public Texture2D Tex
        {
            get
            {
                if (tex != null)
                    return tex;
                else
                {
                    tex = ContentLoader.Get_texture(this);
                    return tex;
                }
            }
        }
        public Vector2 Size { get { return new Vector2(Width, Height); } }
        public int Width { get { return this.Tex.Width; } }
        public int Height { get { return this.Tex.Height; } }
    }
}
