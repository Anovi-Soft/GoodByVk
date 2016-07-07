using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoodByVk.Api;

namespace GoodByVk
{
    class BotGoodBy
    {
        private const string FilePath = "logs.txt";
        private const string Message =
            "Я больше не захожу в эту социальную сеть\n" +
            "Если есть необходимость мне написать пишите в telegram\n" +
            "Мой телефон: 89655224401\n" + 
            "Это сообщение создано автоматически. Отвечать на него не нужно";
        private static VkApi api;
        private static HashSet<int> marked;


        public static void GoodBy(VkApi vkApi)
        {
            marked = ReadFromFile();
            api = vkApi;
            MarkAllAsRead();
            while (true)
            {
                GoodByStep();
                NewFriendsStep();
                Thread.Sleep(1000);
            }
        }

        private static void NewFriendsStep() =>
            api
                .GetNewFriends()
                .ForEach(x =>
                {
                    MarkAndSendMessage(x);
                    api.DeleteFromFriends(x);
                });

        private static void GoodByStep() => 
            api
                .GetDialogs()
                .Items
                .Where(x=>!x.Message.IsChat)
                .Select(x => x.Message.UserId)
                .Where(x => !marked.Contains(x))
                .ToList()
                .ForEach(MarkAndSendMessage);

        private static void MarkAndSendMessage(int x)
        {
            marked.Add(x);
            LogToFile(x);
            api.SendMessage(x, Message);
        }

        private static HashSet<int> ReadFromFile() =>
            File.Exists(FilePath)
                ? new HashSet<int>(File.ReadAllLines(FilePath).Select(int.Parse))
                : new HashSet<int>();

        private static void LogToFile(int i) =>
            File.AppendAllText(FilePath, i.ToString());

        private static void MarkAllAsRead()
        {
            var unreads = api.GetDialogs()
                .Items
                .Select(x => x.Message.Id)
                .ToArray();
            api.MarkAsRead(unreads);
        }
    }
}
