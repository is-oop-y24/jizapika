using System.Collections.Generic;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.StorageAlgorithm;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.RepositoryExtra;

namespace BackupsExtra.Tools.StorageAlgorithmExtra
{
    public class SingleStorageExtra : SingleStorage, IStorageAlgorithmExtra
    {
        public StorageAlgorithmExtraType GetStorageAlgorithmExtraType()
            => StorageAlgorithmExtraType.SingleType;

        public List<StorageExtra> GetStorages(JobObjects sourceRepository, IRepositoryExtra repository, string backUpName, string restorePointName)
        {
            throw new System.NotImplementedException();
        }
    }
}