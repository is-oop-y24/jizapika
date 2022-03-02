using System.Collections.Generic;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.StorageAlgorithm;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.RepositoryExtra;

namespace BackupsExtra.Tools.StorageAlgorithmExtra
{
    public class SplitStoragesExtra : SplitStorages, IStorageAlgorithmExtra
    {
    public StorageAlgorithmExtraType GetStorageAlgorithmExtraType()
        => StorageAlgorithmExtraType.SplitType;

    public List<StorageExtra> GetStorages(JobObjects sourceRepository, IRepositoryExtra repository, string backUpName, string restorePointName)
    {
        var storages = new List<StorageExtra>();
        foreach (JobObject jobObject in sourceRepository.JobObjectsImmutableList)
        {
            StorageExtra storage = repository.CopyObject(jobObject, 0, GetStorageAlgorithmExtraType());
            var algorithmStorageList = new List<StorageExtra>() { storage };
            storages.Add(repository.CompressingObjects(algorithmStorageList, backUpName, restorePointName, repository.ObjectNameWithoutExtension(jobObject.Way)));
        }

        return storages;
    }
    }
}