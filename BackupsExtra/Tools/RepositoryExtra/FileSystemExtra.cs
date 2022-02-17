using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.StorageAlgorithmExtra;

namespace BackupsExtra.Tools.RepositoryExtra
{
    public class FileSystemExtra : FileSystem, IRepositoryExtra
    {
        public FileSystemExtra(string root)
            : base(root)
        {
        }

        public List<StorageExtra> UnCompressingObjectsToOriginalLocation(StorageExtra storageExtra)
        {
            var storages = new List<StorageExtra>();
            foreach (string originalWay in storageExtra.ImmutableOriginalWays)
            {
                string way = Path.Combine(originalWay, ObjectNameWithoutExtension(originalWay));
                if (storageExtra.CanGetId())
                {
                    var storage = new StorageExtra(way, false, storageExtra.GetId(), storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName, new List<string>());
                    storages.Add(storage);
                    UnCompressingObject(storageExtra.CompressingName, originalWay);
                }
            }

            return storages;
        }

        public List<StorageExtra> UnCompressingObjectsToDifferentLocation(StorageExtra storageExtra, string locationWay)
        {
            var storages = new List<StorageExtra>();
            if (!Directory.Exists(locationWay)) Directory.CreateDirectory(locationWay);
            foreach (string originalWay in storageExtra.ImmutableOriginalWays)
            {
                string way = Path.Combine(locationWay, ObjectNameWithoutExtension(originalWay));
                if (storageExtra.CanGetId())
                {
                    var storage = new StorageExtra(way, false, storageExtra.GetId(), storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName, new List<string>());
                    storages.Add(storage);
                    UnCompressingObject(storageExtra.CompressingName, locationWay);
                }
            }

            return storages;
        }

        public void UnCompressingObject(string sourceFile, string targetDirectory)
        {
            ZipArchive zipArchive = ZipFile.Open(sourceFile, ZipArchiveMode.Update);
            zipArchive.ExtractToDirectory(targetDirectory, true);
        }

        public bool CanUncompressing(StorageExtra storageExtra)
            => storageExtra.IsZipping;

        public StorageExtra CompressingObjects(
            List<StorageExtra> storages,
            string backUpName,
            string restorePointName,
            string compressedName,
            StorageAlgorithmExtraType storageAlgorithmExtra)
        {
            string fakeDirectoryName = PackStoragesToRestorePoint(storages, backUpName, restorePointName + "_fake");
            string normalDirectoryName = Path.Combine(Root, backUpName, restorePointName);
            Directory.CreateDirectory(normalDirectoryName);
            string compressedFile = Path.Combine(normalDirectoryName, compressedName) + ".zip";
            ZipFile.CreateFromDirectory(fakeDirectoryName, compressedFile);
            Directory.Delete(fakeDirectoryName, true);
            return new StorageExtra(compressedFile, true, storages[0].GetId(), storageAlgorithmExtra, compressedName, storages.Select(storage => storage.Way).ToList());
        }

        public StorageExtra CopyObject(JobObject jobObject, uint id, StorageAlgorithmExtraType storageAlgorithmExtraType)
        {
            if (!File.Exists(jobObject.Way))
                throw new BackUpsExtraExceptions($"Not correct file name: {jobObject.Way}.");
            return new StorageExtra(jobObject.Way, false, id, storageAlgorithmExtraType, jobObject.Way, new List<string>());
        }

        public void MergeTwoRestorePointExtras(
            RestorePointExtra oldRestorePointExtra,
            RestorePointExtra newRestorePointExtra,
            BackUpExtra backUpExtra,
            bool isSplitAlgorithm)
        {
            if (isSplitAlgorithm)
            {
                foreach (StorageExtra oldStorageExtra in oldRestorePointExtra.ImmutableStorages)
                {
                    bool isInNewRestorePoint = true;
                    foreach (StorageExtra newStorageExtra in newRestorePointExtra.ImmutableStorages)
                    {
                        if (oldStorageExtra.GetId() == newStorageExtra.GetId())
                        {
                            DeleteStorageExtraFromRepository(oldStorageExtra);
                            isInNewRestorePoint = false;
                            break;
                        }
                    }

                    if (isInNewRestorePoint)
                    {
                        StorageExtra storageExtra = CopyStorageExtra(oldStorageExtra);
                        DeleteStorageExtraFromRepository(oldStorageExtra);
                        newRestorePointExtra.AddStorage(storageExtra);
                    }
                }
            }

            foreach (RestorePointExtra restorePointExtra in backUpExtra.ImmutableRestorePointExtraList)
            {
                if (restorePointExtra.IsTheSameIdWith(oldRestorePointExtra))
                {
                    foreach (StorageExtra storage in restorePointExtra.ImmutableStorages)
                    {
                        DeleteStorageExtraFromRepository(storage);
                    }

                    backUpExtra.DeleteRestorePoint(restorePointExtra);
                    string directoryWay = Path.Combine(Root, backUpExtra.Name, restorePointExtra.RestorePointName);
                    Directory.Delete(directoryWay);
                }
            }
        }

        public void DeleteStorageExtraFromRepository(StorageExtra storageExtra)
        {
            if (!CanDeleteStorageExtraFromRepository(storageExtra))
                throw new BackUpsExtraExceptions("Storage can't be deleted");
            RemoveFile(storageExtra.CompressingName);
        }

        public bool CanDeleteStorageExtraFromRepository(StorageExtra storageExtra)
            => CanRemoveFile(storageExtra.CompressingName);

        public StorageExtra CopyStorageExtra(StorageExtra storageExtra)
        {
            if (storageExtra.CanGetId()) return new StorageExtra(storageExtra.Way, storageExtra.IsZipping, storageExtra.GetId(), storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName, storageExtra.ImmutableOriginalWays.ToList());
            return new StorageExtra(storageExtra.Way, storageExtra.IsZipping, 0, storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName, storageExtra.ImmutableOriginalWays.ToList());
        }

        public StorageExtra CompressingObjects(List<StorageExtra> storages, string backUpName, string restorePointName, string compressedName)
        {
            string fakeDirectoryName = PackStoragesToRestorePoint(storages, backUpName, restorePointName + "_fake");
            string normalDirectoryName = Path.Combine(Root, backUpName, restorePointName);
            Directory.CreateDirectory(normalDirectoryName);
            string compressedFile = Path.Combine(normalDirectoryName, compressedName) + ".zip";
            ZipFile.CreateFromDirectory(fakeDirectoryName, compressedFile);
            Directory.Delete(fakeDirectoryName, true);
            return new StorageExtra(compressedFile, true, storages[0].GetId(), storages[0].StorageAlgorithmExtraType, compressedName, storages.Select(storageExtra => storageExtra.Way).ToList());
        }

        protected string PackStoragesToRestorePoint(
            List<StorageExtra> storages, string backUpName, string restorePointName)
        {
            string directoryWay = Path.Combine(Root, backUpName, restorePointName);
            Directory.CreateDirectory(directoryWay);
            foreach (Storage storage in storages)
            {
                File.Create(Path.Combine(directoryWay, ObjectNameWithExtension(storage.Way))).Close();
            }

            return directoryWay;
        }

        protected void RemoveFile(string path)
        {
            if (!CanRemoveFile(path)) throw new BackUpsExtraExceptions("File doesn't exist.");
            File.Delete(path);
        }

        protected bool CanRemoveFile(string path)
            => File.Exists(path);
    }
}