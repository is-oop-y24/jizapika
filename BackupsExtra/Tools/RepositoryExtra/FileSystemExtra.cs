using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Tools.BackUpClasses;
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
            throw new System.NotImplementedException();
        }

        public List<StorageExtra> UnCompressingObjectsToDifferentLocation(StorageExtra storageExtra, string locationWay)
        {
            throw new System.NotImplementedException();
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

        public void MergeTwoRestorePointExtras(
            RestorePointExtra oldRestorePointExtra,
            RestorePointExtra newRestorePointExtra,
            BackUpExtra backUpExtra,
            bool isSplitAlgorithm)
        {
            if (isSplitAlgorithm)
            {
                foreach (StorageExtra oldStorageExtra in oldRestorePointExtra.StoragesExtra)
                {
                    bool isInNewRestorePoint = true;
                    foreach (StorageExtra newStorageExtra in newRestorePointExtra.StoragesExtra)
                    {
                        if (oldStorageExtra.GetId() == newStorageExtra.GetId())
                        {
                            oldRestorePointExtra.StoragesExtra.Remove(oldStorageExtra);
                            DeleteStorageExtraFromRepository(oldStorageExtra);
                            isInNewRestorePoint = false;
                            break;
                        }
                    }

                    if (isInNewRestorePoint)
                    {
                        StorageExtra storageExtra = CopyStorageExtra(oldStorageExtra);
                        oldRestorePointExtra.StoragesExtra.Remove(oldStorageExtra);
                        DeleteStorageExtraFromRepository(oldStorageExtra);
                        newRestorePointExtra.StoragesExtra.Add(storageExtra);
                    }
                }
            }

            foreach (RestorePointExtra restorePointExtra in backUpExtra.ImmutableRestorePointExtraList)
            {
                if (restorePointExtra.IsTheSameIdWith(oldRestorePointExtra))
                {
                    foreach (StorageExtra storage in restorePointExtra.StoragesExtra)
                    {
                        DeleteStorageExtraFromRepository(storage);
                    }

                    backUpExtra.DeleteRestorePoint(restorePointExtra);
                }
            }
        }

        public void DeleteStorageExtraFromRepository(StorageExtra storageExtra)
        {
            if (!CanDeleteStorageExtraFromRepository(storageExtra))
                throw new BackUpsExtraExceptions("Storage can't be deleted");
            File.Delete(storageExtra.CompressingName);
        }

        public bool CanDeleteStorageExtraFromRepository(StorageExtra storageExtra)
            => CanRemoveFile(storageExtra.CompressingName);

        public StorageExtra CopyStorageExtra(StorageExtra storageExtra)
        {
            if (storageExtra.CanGetId()) return new StorageExtra(storageExtra.Way, storageExtra.IsZipping, storageExtra.GetId(), storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName, storageExtra.ImmutableOriginalWays.ToList());
            return new StorageExtra(storageExtra.Way, storageExtra.IsZipping, 0, storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName, storageExtra.ImmutableOriginalWays.ToList());
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
            File.Delete(path);
        }

        protected bool CanRemoveFile(string path)
            => File.Exists(path);

        protected bool CanReplaceFile(string currentPath, string nextPath)
            => File.Exists(currentPath);

        protected void ReplaceFile(string currentPath, string nextPath)
        {
            File.Move(currentPath, nextPath, true);
        }
    }
}