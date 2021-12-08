using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;

namespace Backups.Tools.StorageAlgorithm
{
    public class SplitStorages : IStorageAlgorithm
    {
        public List<Storage> GetStorages(
            JobObjects sourceRepository, IRepository repository, string backUpName, string restorePointName)
        {
            var storages = new List<Storage>();
            foreach (JobObject jobObject in sourceRepository.JobObjectsImmutableList)
            {
                Storage storage = repository.CopyObject(jobObject);
                var algorithmStorageList = new List<Storage>() { storage };
                storages.Add(repository.CompressingObjects(algorithmStorageList, backUpName, restorePointName));
            }

            return storages;
        }
    }
}