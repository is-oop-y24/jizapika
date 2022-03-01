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
            var storages = new List<StorageExtra>();
            uint storageId = 1;
            foreach (JobObject jobObject in sourceRepository.JobObjectsImmutableList)
            {
                StorageExtra storage = repository.CopyObject(jobObject, storageId, GetStorageAlgorithmExtraType());
                storages.Add(storage);
                storageId++;
            }

            StorageExtra algorithmStorage =
                repository.CompressingObjects(storages, backUpName, restorePointName, string.Empty);
            return new List<StorageExtra>() { algorithmStorage };
        }
    }
}