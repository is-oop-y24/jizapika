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
        }

        public new ImmutableList<StorageExtra> ImmutableStorages => Storages.ToImmutableList();
        [JsonProperty]
        protected new List<StorageExtra> Storages { get; set; }

        public bool IsTheSameIdWith(RestorePointExtra restorePointExtra)
            => restorePointExtra.Id == Id;

        public void AddStorage(StorageExtra storage)
        {
            Storages.Add(storage);
        }
    }
}