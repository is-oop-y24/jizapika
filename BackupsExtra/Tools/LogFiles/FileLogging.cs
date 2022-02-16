using System.IO;
using NUnit.Framework.Internal;

namespace BackupsExtra.Tools.LogFiles
{
    public class FileLogging : ILogging
    {
        private bool _isNeedTime;
        private string _path;
        private readonly Logger _logger;
        private string _allPath;

        public FileLogging(string path, bool isNeedTime)
        {
            _isNeedTime = isNeedTime;
            _path = path;
            _allPath = Path.Combine(_path, "logs.txt");
            _logger = new Logger(_allPath);
        }

        public void Info(string message)
        {
        }

        public void Warning(string message)
        {
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