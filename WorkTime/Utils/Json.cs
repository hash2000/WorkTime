using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTime.Utils
{
    public static class Json
    {
        public static JsonSerializer CreateSerializer()
        {
            JsonSerializer serializer = new JsonSerializer()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
            };
            serializer.Converters.Add(new IsoDateTimeConverter());

            return serializer;
        }
    }
}