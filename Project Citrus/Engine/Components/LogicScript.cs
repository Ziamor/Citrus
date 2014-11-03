using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.Components
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    class LogicScript : Component
    {
        [JsonProperty(Required = Required.Always)]
        private String script = "fireball_logic.lua";

        public LogicScript() { this.name = "logic_script"; }

        public String Script { get { return script; } set { script = value; } }
    }
}
