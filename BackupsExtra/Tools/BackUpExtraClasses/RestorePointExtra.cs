using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using BackupsExtra.Tools.RepositoryExtra;
using BackupsExtra.Tools.StorageAlgorithmExtra;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.BackUpExtraClasses
{
    public class RestorePointExtra : RestorePoint
    {
        public RestorePointExtra(
            JobObjects jobObjects,
            IStorageAlgorithmExtra algorithmExtra,
            uint id,
            IRepositoryExtra repositoryExtra,
            string backUpName)
            : base(jobObjects, algorithmExtra, id, repositoryExtra, backUpName)
        {
            StoragesExtra = algorithmExtra.GetStorages(jobObjects, repositoryExtra, backUpName, RestorePointName);
            AlgorithmExtra = algorithmExtra;
            RepositoryExtra = repositoryExtra;
            JobObjectsForSerialization = jobObjects;
            BackUpName = backUpName;
        }

        [JsonConstructor]
        private RestorePointExtra(
            JobObjects jobObjectsForSerialization,
            IStorageAlgorithmExtra algorithmExtra,
            uint id,
            IRepositoryExtra repositoryExtra,
            string backUpName,
            List<StorageExtra> storagesExtra = null)
        {
            Id = id;
            RestorePointName = "RestorePoint" + Id;
            Time = DateTime.UtcNow;
            StoragesExtra = storagesExtra ?? algorithmExtra.GetStorages(jobObjectsForSerialization, repositoryExtra, backUpName, RestorePointName);
            AlgorithmExtra = algorithmExtra;
            RepositoryExtra = repositoryExtra;
            JobObjectsForSerialization = jobObjectsForSerialization;
            BackUpName = backUpName;
        }

        [JsonIgnore]
        public new ImmutableList<StorageExtra> ImmutableStorages => StoragesExtra.ToImmutableList();
        [JsonProperty]
        private List<StorageExtra> StoragesExtra { get; set; }
        [JsonProperty]
        private IStorageAlgorithmExtra AlgorithmExtra { get; set; }

        [JsonProperty]
        private IRepositoryExtra RepositoryExtra { get; set; }
        [JsonProperty]
        private JobObjects JobObjectsForSerialization { get; set; }
        [JsonProperty]
        private string BackUpName { get; set; }

        public bool IsTheSameIdWith(RestorePointExtra restorePointExtra)
            => restorePointExtra.Id == Id;

        public void AddStorage(StorageExtra storage)
        {
            StoragesExtra.Add(storage);
        }
    }
}