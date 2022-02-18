using System;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.LogFiles
{
    public class ConsoleLogging : ILogging
    {
        public ConsoleLogging(bool isNeedTime)
        {
            IsNeedTime = isNeedTime;
        }

        [JsonProperty]
        private bool IsNeedTime { get; set; }
        public void Info(string message)
        {
            if (IsNeedTime) message = DateTime.UtcNow + " [Info] " + ": " + message;
            Console.WriteLine(message);
        }

        public void Warning(string message)
        {
            if (IsNeedTime) message = DateTime.UtcNow + " [Warning] " + ": " + message;
            Console.WriteLine(message);
        }

        public void BeginMakingTimeAlert()
        {
            IsNeedTime = true;
        }

        public void EndMakingTimeAlert()
        {
            IsNeedTime = false;
        }
    }
}