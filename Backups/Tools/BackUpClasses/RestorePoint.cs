using System;
using System.Collections.Generic;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using Backups.Tools.StorageAlgorithm;

namespace Backups.Tools.BackUpClasses
{
    public class RestorePoint
    {
        public RestorePoint(
            JobObjects jobObjects,
            IStorageAlgorithm algorithm,
            uint id,
            IRepository repository,
            string backUpName)
        {
            Id = id;
            string restorePointName = "RestorePoint" + Id;
            Storages = algorithm.GetStorages(jobObjects, repository, backUpName, restorePointName);
            Time = DateTime.UtcNow;
        }

        public uint Id { get; }
        public DateTime Time { get; }
        public List<Storage> Storages { get; }
    }
}