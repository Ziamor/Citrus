using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.Components
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    class Position : Component
    {
        [JsonProperty(Required = Required.Always)]
        private int x = 50;
        [JsonProperty(Required = Required.Always)]
        private int y = 20;

        public Position() { this.name = "position"; }

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
    }
}
