using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.RepositoryExtra
{
    public interface IRepositoryExtra : IRepository
    {
        List<StorageExtra> UnCompressingObjectsToOriginalLocation(
            StorageExtra storageExtra, string backUpExtraName, string restorePointExtraName, string compressedName);
        List<StorageExtra> UnCompressingObjectsToDifferentLocation(
            StorageExtra storageExtra, string backUpExtraName, string restorePointExtraName, string compressedName);

        void DeleteJobObject(JobObject jobObject);
        void DeleteStorageExtraFromRepository(StorageExtra storageExtra);
        StorageExtra CopyStorageExtra(StorageExtra storageExtra);

        public void MergeTwoRestorePointExtras(
            RestorePointExtra oldRestorePointExtra,
            RestorePointExtra newRestorePointExtra,
            string backUpExtraName,
            string newRestorePointExtraName);
    }
}