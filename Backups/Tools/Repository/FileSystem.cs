using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Exceptions;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;

namespace Backups.Tools.Repository
{
    public class FileSystem : IRepository
    {
        private string _root;
        public FileSystem(string root)
        {
            if (!Directory.Exists(root)) throw new BackUpsExceptions($"Not correct directory name: {root}");
            _root = root;
        }

        public Storage CopyObject(JobObject jobObject)
        {
            if (!File.Exists(jobObject.Way)) throw new BackUpsExceptions($"Not correct file name: {jobObject.Way}.");
            return new Storage(jobObject.Way, false);
        }

        public Storage CompressingObjects(List<Storage> storages, string backUpName, string restorePointName)
        {
            string directoryName = PackStoragesToRestorePoint(storages, backUpName, restorePointName);
            string compressedFile = directoryName + ".zip";
            ZipFile.CreateFromDirectory(directoryName, compressedFile);
            Directory.Delete(directoryName, true);
            return new Storage(compressedFile, true);
        }

        private string PackStoragesToRestorePoint(
            List<Storage> storages, string backUpName, string restorePointName)
        {
            string directoryWay = Path.Combine(_root, backUpName, restorePointName);
            Directory.CreateDirectory(directoryWay);
            foreach (Storage storage in storages)
            {
                FileStream fstream = File.Create(Path.Combine(directoryWay, ObjectName(storage.Way)));
                fstream.Close();
            }

            return directoryWay;
        }

        private string ObjectName(string way)
        {
            try
            {
                return Path.GetFileName(way);
            }
            catch (FormatException)
            {
                throw new BackUpsExceptions($"Not correct file name: {way}.");
            }
        }
    }
}