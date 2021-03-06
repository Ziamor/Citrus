﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.Components
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    class Health : Component
    {
        [JsonProperty(Required = Required.Always)]
        public int hp = 100;
        public Health() { this.name = "health"; }
        public int HP { get { return hp; } set { hp = value; } }
    }
}
