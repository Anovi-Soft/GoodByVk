using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoodByVk.Api
{
    public class JsonGetDialogs
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("items")]
        public List<Dialogs> Items { get; set; }

        public class Dialogs
        {
            [JsonProperty("unread")]
            public int Unread { get; set; }
            
            [JsonProperty("message")]
            public Message Message { get; set; }

            [JsonProperty("in_read")]
            public int InRead { get; set; }

            [JsonProperty("out_read")]
            public int OutRead { get; set; }
        }
    }
}
