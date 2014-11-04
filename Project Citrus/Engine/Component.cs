using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project_Citrus.Engine.ContentLoading;

namespace Project_Citrus.Engine
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn), JsonConverter(typeof(ComponentConverter))]
    public class Component
    {
        protected String name;
        public String Name { get { return name; } }
    }
}
