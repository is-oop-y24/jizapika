using System;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.LogFiles
{
    public class ConsoleLogging
    {
        [JsonProperty]
        private bool _isNeedTime;

        public ConsoleLogging(bool isNeedTime)
        {
            _isNeedTime = isNeedTime;
        }

        public void Info(string message)
        {
            if (_isNeedTime) message = DateTime.UtcNow + " [Warning] " + ": " + message;
            Console.WriteLine(message);
        }

        public void Warning(string message)
        {
            if (_isNeedTime) message = DateTime.UtcNow + " [Warning] " + ": " + message;
            Console.WriteLine(message);
        }

        public void BeginMakingTimeAlert()
        {
            _isNeedTime = true;
        }

        public void EndMakingTimeAlert()
        {
            _isNeedTime = false;
        }
    }
}