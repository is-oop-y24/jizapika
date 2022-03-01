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
        public RestorePoint(JobObjects jobObjects, IStorageAlgorithm algorithm, uint id, IRepository repository, string backUpName)
        {
            Id = id;
            RestorePointName = "RestorePoint" + Id;
            Storages = algorithm.GetStorages(jobObjects, repository, backUpName, RestorePointName);
            Time = DateTime.UtcNow;
        }

        protected RestorePoint()
        {
        }

        public DateTime Time { get; set; }
        public string RestorePointName { get; set; }
        [JsonIgnore]
        public ImmutableList<Storage> ImmutableStorages => Storages.ToImmutableList();
        [JsonProperty]
        protected uint Id { get; set; }
        private List<Storage> Storages { get; }
    }
}