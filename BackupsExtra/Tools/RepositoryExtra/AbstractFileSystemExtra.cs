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
            => 

        public void MergeTwoRestorePointExtras(
            RestorePointExtra oldRestorePointExtra,
            RestorePointExtra newRestorePointExtra,
            string backUpExtraName,
            string newRestorePointExtraName)
        {
            foreach (StorageExtra oldStorageExtra in oldRestorePointExtra.StoragesExtra)
            {
                bool flag = true;
                foreach (StorageExtra newStorageExtra in newRestorePointExtra.StoragesExtra)
                {
                    if (oldStorageExtra.GetId() == newStorageExtra.GetId())
                    {
                        oldRestorePointExtra.StoragesExtra.Remove(oldStorageExtra);
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    StorageExtra storageExtra = CopyStorageExtra(oldStorageExtra);
                    oldRestorePointExtra.StoragesExtra.Remove(oldStorageExtra);
                    newRestorePointExtra.StoragesExtra.Add(storageExtra);
                }
            }
        }
    }
}