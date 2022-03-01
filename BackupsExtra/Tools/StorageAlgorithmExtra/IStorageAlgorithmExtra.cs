using System.Collections.Generic;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.StorageAlgorithm;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.RepositoryExtra;

namespace BackupsExtra.Tools.StorageAlgorithmExtra
{
    public interface IStorageAlgorithmExtra : IStorageAlgorithm
    {
        public StorageAlgorithmExtraType GetStorageAlgorithmExtraType();
        public List<StorageExtra> GetStorages(
            JobObjects sourceRepository, IRepositoryExtra repository, string backUpName, string restorePointName);
    }
}