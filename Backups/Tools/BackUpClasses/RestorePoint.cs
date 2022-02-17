using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using Backups.Tools.StorageAlgorithm;
using Newtonsoft.Json;

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
        public ImmutableList<Storage> ImmutableStorages => Storages.ToImmutableList();
        protected uint Id { get; set; }
        protected List<Storage> Storages { get; set; }
    }
}