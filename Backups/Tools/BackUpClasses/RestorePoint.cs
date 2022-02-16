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
            RestorePointName = "RestorePoint" + Id;
            Storages = algorithm.GetStorages(jobObjects, repository, backUpName, RestorePointName);
            Time = DateTime.UtcNow;
        }

        public DateTime Time { get; protected set; }
        public string RestorePointName { get; }
        protected uint Id { get; }
        protected List<Storage> Storages { get; }
    }
}