using System.Collections.Generic;
using System.IO;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.RepositoryExtra
{
    public class AbstractFileSystemExtra : AbstractFileSystem, IRepositoryExtra
    {
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
            if (storageExtra.CanGetId()) return new StorageExtra(storageExtra.Way, storageExtra.IsZipping, storageExtra.GetId(), storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName);
            return new StorageExtra(storageExtra.Way, storageExtra.IsZipping, 0, storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName);
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
    }
}