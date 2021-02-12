using System;
using CitizenFX.Core;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;

namespace MPEventFramework
{
    class Utils : BaseScript
    {
        public static void Log(string data)
        {
            Debug.WriteLine(String.Format("BASE: {0}", data));
            SendChatMessage(data);
        }
        public static int GetRandom(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }
        public static void TriggerClientEvent(string eventName, params object[] args)
        {
            TriggerEvent(eventName, args);
        }
        public static void SendChatMessage(string message)
        {
            TriggerClientEvent("chat:addMessage", new
            {
                color = new[] { 255, 255, 255 },
                multiline = true,
                args = new[] { "MP BASE:", message }
            });
        }
    }
}
