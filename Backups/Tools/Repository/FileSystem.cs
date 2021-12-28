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
            if (!Directory.Exists(root))
                throw new BackUpsExceptions($"Not correct directory name: {root}");
            _root = root;
        }

        public Storage CopyObject(JobObject jobObject)
        {
            if (!File.Exists(jobObject.Way))
                throw new BackUpsExceptions($"Not correct file name: {jobObject.Way}.");
            return new Storage(jobObject.Way, false);
        }

        public Storage CompressingObjects(
            List<Storage> storages, string backUpName, string restorePointName, string compressedName)
        {
            string fakeDirectoryName = PackStoragesToRestorePoint(storages, backUpName, restorePointName + "_fake");
            string normalDirectoryName = Path.Combine(_root, backUpName, restorePointName);
            Directory.CreateDirectory(normalDirectoryName);
            string compressedFile = Path.Combine(normalDirectoryName, compressedName) + ".zip";
            ZipFile.CreateFromDirectory(fakeDirectoryName, compressedFile);
            Directory.Delete(fakeDirectoryName, true);
            return new Storage(compressedFile, true);
        }

        public string ObjectNameWithoutExtension(string way)
        {
            try
            {
                return Path.GetFileNameWithoutExtension(way);
            }
            catch (FormatException)
            {
                throw new BackUpsExceptions($"Not correct file name: {way}.");
            }
        }

        private string PackStoragesToRestorePoint(
            List<Storage> storages, string backUpName, string restorePointName)
        {
            string directoryWay = Path.Combine(_root, backUpName, restorePointName);
            Directory.CreateDirectory(directoryWay);
            foreach (Storage storage in storages)
            {
                File.Create(Path.Combine(directoryWay, ObjectNameWithExtension(storage.Way))).Close();
            }

            return directoryWay;
        }

        private string ObjectNameWithExtension(string way)
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