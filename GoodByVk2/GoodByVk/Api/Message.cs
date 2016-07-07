using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoodByVk.Api
{
    public class Message
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("date")]
        public int Date { get; set; }

        [JsonProperty("out")]
        public int Out { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("read_state")]
        public int ReadState { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
        
        [JsonProperty("chat_id")]
        public int ChatId { get; set; }

        [JsonIgnore]
        public bool IsChat => ChatId != 0;
    }
}
