using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine
{
    [JsonObject(MemberSerialization.OptIn), JsonConverter(typeof(ComponentConverter))]
    class Component
    {
        protected String name;
        public String Name { get { return name; } }
    }
}
