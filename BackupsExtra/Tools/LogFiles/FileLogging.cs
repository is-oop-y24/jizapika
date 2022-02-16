using System;
using System.IO;

namespace BackupsExtra.Tools.LogFiles
{
    public class FileLogging : ILogging
    {
        private bool _isNeedTime;
        private string _path;
        private string _allPath;

        public FileLogging(string pathDirectory, bool isNeedTime)
        {
            _isNeedTime = isNeedTime;
            _path = pathDirectory;
            _allPath = Path.Combine(_path, "logs.txt");
        }

        public void Info(string message)
        {
            if (_isNeedTime) message = DateTime.UtcNow + " [Info] " + ": " + message;
            using (StreamWriter fileStream = File.AppendText(_allPath))
            {
                fileStream.WriteLine(message);
            }
        }

        public void Warning(string message)
        {
            if (_isNeedTime) message = DateTime.UtcNow + " [Warning] " + ": " + message;
            using (StreamWriter fileStream = File.AppendText(_allPath))
            {
                fileStream.WriteLine(message);
            }
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