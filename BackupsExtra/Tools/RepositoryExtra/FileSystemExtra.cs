using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.RepositoryExtra
{
    public class FileSystemExtra : FileSystem, IRepositoryExtra
    {
        public FileSystemExtra(string root)
            : base(root)
        {
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
            throw new System.NotImplementedException();
        }

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
                        DeleteStorageExtraFromRepository(oldStorageExtra);
                        oldRestorePointExtra.StoragesExtra.Remove(oldStorageExtra);
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    StorageExtra storageExtra = CopyStorageExtra(oldStorageExtra);
                }
            }
        }
    }
}