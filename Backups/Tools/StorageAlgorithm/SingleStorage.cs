using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;

namespace Backups.Tools.StorageAlgorithm
{
    public class SingleStorage : IStorageAlgorithm
    {
        public List<Storage> GetStorages(
            JobObjects sourceRepository, IRepository repository, string backUpName, string restorePointName)
        {
            var storages = new List<Storage>();
            foreach (JobObject jobObject in sourceRepository.JobObjectsImmutableList)
            {
                Storage storage = repository.CopyObject(jobObject);
                storages.Add(storage);
            }

            Storage algorithmStorage = repository.CompressingObjects(storages, backUpName, restorePointName);
            return new List<Storage>() { algorithmStorage };
        }
    }
}