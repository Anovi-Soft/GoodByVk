using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoodByVk.Api
{
    class VkJson<T>
    {
        [JsonProperty("response")]
        public T Response { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}
