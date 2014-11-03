using Project_Citrus.Engine;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.Components
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    class Image : Component
    {
        [JsonProperty(Required = Required.Always)]
        public String image_name = null;
        public Image() { this.name = "image"; }
        public Image(String image_name)
            : base()
        {
            this.name = "image";
            this.image_name = image_name;
        }

        public Texture2D Tex
        {
            get
            {
                return ContentLoader.Get_texture(this.image_name);
            }
        }
    }
}
