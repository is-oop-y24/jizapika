using System;
using System.IO;

namespace BackupsExtra.Tools.LogFiles
{
    public class ConsoleLogging
    {
        private bool _isNeedTime;

        public ConsoleLogging(string pathDirectory, bool isNeedTime)
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