using Project_Citrus.Engine;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project_Citrus.Engine.ContentLoading;
using Project_Citrus.ContentLoading;

namespace Project_Citrus.Engine.Components
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    class Image : Component
    {
        [JsonProperty(Required = Required.Always)]
        public String image_name = null;
        private ImageInfo image_info = null;
        public Image() { this.name = "image"; }
        public Image(String image_name)
            : base()
        {
            this.name = "image";
            this.image_name = image_name;
        }
        public ImageInfo Image_Info{
            get{
                if (image_info != null)
                    return image_info;
                else
                {
                    image_info = ContentLoader.Get_ImageInfo(image_name);
                    return image_info;
                }
            }
        }
    }
}
