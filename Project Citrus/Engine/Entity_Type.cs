using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    [JsonObject(MemberSerialization.OptIn)]
    class Entity_Type
    {
        [JsonProperty(Required = Required.Always)]
        private String name = "";
        public Entity_Type(String name)
        {
            this.name = name;
        }

        public String Name
        {
            get { return name; }
        }
    }
}
