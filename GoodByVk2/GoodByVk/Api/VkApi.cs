using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace GoodByVk.Api
{
    public class VkApi
    {
        private readonly string token;

        public VkApi(string token)
        {
            this.token = token;
        }

        private string Get(string method, string param = null)
        {
            var url = "https://api.vk.com/method/" + method + "?" + param +
                $"&v=5.52&access_token={token}";
            var req = WebRequest.Create(url);
            var resp = req.GetResponse();
            var stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            var Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }

        public JsonGetDialogs GetDialogs(int unread = 1)
        {
            var line = Get("messages.getDialogs", $"unread={unread}");
            var json = JsonConvert.DeserializeObject<VkJson<JsonGetDialogs>>(line);
            return json.Response;
        }

        public int SendMessage(int userId, string message)
        {
            var line = Get("messages.send", $"user_id={userId}&message={message}");
            var json = JsonConvert.DeserializeObject<VkJson<int>>(line);
            return json.Response;
        }

        public bool MarkAsRead(params int[] messageIds)
        {
            var line = Get("messages.markAsRead", $"message_ids={string.Join(",", messageIds)}");
            var json = JsonConvert.DeserializeObject<VkJson<int>>(line);
            return json.Response == 1;
        }

        public List<int> GetNewFriends()
        {
            var line = Get("friends.getRequests");
            var json = JsonConvert.DeserializeObject<VkJson<VkJsonCollection<int>>>(line);
            return json.Response.Items;
        }

        public void DeleteFromFriends(int userId) =>
            Get("friends.delete", $"user_id={userId}");
    }
}
