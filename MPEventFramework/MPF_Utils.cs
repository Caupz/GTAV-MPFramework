using System;
using CitizenFX.Core;

namespace MPFrameworkClient
{
    public class MPF_Utils : BaseScript
    {
        public static void Log(string data, string prefix = "")
        {
            if(prefix == "")
            {
                prefix = "Framework";
            }
            Debug.WriteLine(String.Format("{0}:{1}", prefix, data));
            SendChatMessage(data, prefix);
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
        public static void SendChatMessage(string message, string prefix)
        {
            TriggerClientEvent("chat:addMessage", new
            {
                color = new[] { 255, 255, 255 },
                multiline = true,
                args = new[] { prefix+":", message }
            });
        }
    }
}
