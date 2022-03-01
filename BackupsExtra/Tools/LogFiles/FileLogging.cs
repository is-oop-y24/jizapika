using System;
using System.IO;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.LogFiles
{
    public class FileLogging : ILogging
    {
        private string _allPath;

        public FileLogging(string pathDirectory, bool isNeedTime)
        {
            IsNeedTime = isNeedTime;
            PathDirectory = pathDirectory;
            _allPath = Path.Combine(PathDirectory, "logs.txt");
        }

        [JsonProperty]
        private bool IsNeedTime { get; set; }
        [JsonProperty]
        private string PathDirectory { get; }
        public void Info(string message)
        {
            if (IsNeedTime) message = DateTime.UtcNow + " [Info] " + ": " + message;
            using StreamWriter fileStream = File.AppendText(_allPath);
            fileStream.WriteLine(message);
        }

        public void Warning(string message)
        {
            if (IsNeedTime) message = DateTime.UtcNow + " [Warning] " + ": " + message;
            using StreamWriter fileStream = File.AppendText(_allPath);
            fileStream.WriteLine(message);
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