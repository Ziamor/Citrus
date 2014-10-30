using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Project_Citrus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    [JsonObject(MemberSerialization.OptIn)]
    class Sprite
    {
        [JsonProperty(Required = Required.Always)]
        private String name = null;
        [JsonProperty(Required = Required.Always)]
        private String path = null;

        public Sprite()
        {

        }
        public Sprite(String name, String path)
        {
            this.name = name;
            this.path = path;            
        }
        public String Path
        {
            get { return path; }
        }

        public Texture2D Tex
        {
            get
            {
                if (path != null)
                    return ContentLoader.get_texture(@path);
                else
                    return null;
            }
        }
    }
}
