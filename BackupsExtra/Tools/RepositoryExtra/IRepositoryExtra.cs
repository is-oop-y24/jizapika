using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.StorageAlgorithmExtra;

namespace BackupsExtra.Tools.RepositoryExtra
{
    public interface IRepositoryExtra : IRepository
    {
        List<StorageExtra> UnCompressingObjectsToOriginalLocation(StorageExtra storageExtra);
        List<StorageExtra> UnCompressingObjectsToDifferentLocation(StorageExtra storageExtra, string locationWay);
        public void UnCompressingObject(string sourceFile, string targetDirectory);
        bool CanUncompressing(StorageExtra storageExtra);
        void DeleteStorageExtraFromRepository(StorageExtra storageExtra);
        StorageExtra CopyStorageExtra(StorageExtra storageExtra);
        StorageExtra CompressingObjects(List<StorageExtra> storages, string backUpName, string restorePointName, string compressedName);
        public StorageExtra CopyObject(JobObject jobObject, uint id, StorageAlgorithmExtraType storageAlgorithmExtraType);

        public void MergeTwoRestorePointExtras(
            RestorePointExtra oldRestorePointExtra,
            RestorePointExtra newRestorePointExtra,
            BackUpExtra backUpExtra,
            bool isSplitAlgorithm);
    }
}