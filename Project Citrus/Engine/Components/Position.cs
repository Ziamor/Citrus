using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.Components
{
    [JsonObject(MemberSerialization.OptIn)]
    class Position : Component
    {
        [JsonProperty(Required = Required.Always)]
        public int x = 50;
        [JsonProperty(Required = Required.Always)]
        public int y = 20;

        public Position() { this.name = "position"; }
    }
}
