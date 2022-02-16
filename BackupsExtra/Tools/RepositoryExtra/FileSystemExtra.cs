using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
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

        public List<Storage> UnCompressingObjectsToOriginalLocation(
            Storage storage,
            string backUpName,
            string restorePointName,
            string compressedName)
        {
            throw new System.NotImplementedException();
        }

        public List<Storage> UnCompressingObjectsToDifferentLocation(
            Storage storage,
            string backUpName,
            string restorePointName,
            string compressedName)
        {
            throw new System.NotImplementedException();
        }

        public List<StorageExtra> UnCompressingObjectsToOriginalLocation(
            StorageExtra storageExtra,
            string backUpExtraName,
            string restorePointExtraName,
            string compressedName)
        {
            throw new System.NotImplementedException();
        }

        public List<StorageExtra> UnCompressingObjectsToDifferentLocation(
            StorageExtra storageExtra,
            string backUpExtraName,
            string restorePointExtraName,
            string compressedName)
        {
            throw new System.NotImplementedException();
        }

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
            return new StorageExtra(compressedFile, true, storages[0].GetId(), storageAlgorithmExtra, compressedName);
        }

        public void MergeTwoRestorePointExtras(
            RestorePointExtra oldRestorePointExtra,
            RestorePointExtra newRestorePointExtra,
            BackUpExtra backUpExtra,
            string backUpExtraName,
            string newRestorePointExtraName,
            string compressedName,
            bool isSplitAlgorithm)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteJobObject(JobObject jobObject)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteStorageExtraFromRepository(StorageExtra storageExtra)
        {
            throw new System.NotImplementedException();
        }

        public StorageExtra CopyStorageExtra(StorageExtra storageExtra)
        {
            throw new System.NotImplementedException();
        }

        public void MergeTwoRestorePointExtras(
            RestorePointExtra oldRestorePointExtra,
            RestorePointExtra newRestorePointExtra,
            BackUpExtra backUpExtra,
            string backUpExtraName,
            string newRestorePointExtraName,
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
                            isInNewRestorePoint = false;
                            break;
                        }
                    }

                    if (isInNewRestorePoint)
                    {
                        StorageExtra storageExtra = CopyStorageExtra(oldStorageExtra);
                        oldRestorePointExtra.StoragesExtra.Remove(oldStorageExtra);
                        newRestorePointExtra.StoragesExtra.Add(storageExtra);
                    }
                }
            }

            foreach (RestorePointExtra restorePointExtra in backUpExtra.ImmutableRestorePointExtraList)
            {
                if (restorePointExtra.IsTheSameIdWith(oldRestorePointExtra))
                {
                    backUpExtra.DeleteRestorePoint(restorePointExtra);
                    return;
                }
            }
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
    }
}