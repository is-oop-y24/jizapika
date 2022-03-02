using System.Collections.Generic;
using System.Collections.Immutable;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.RepositoryExtra;
using BackupsExtra.Tools.StorageAlgorithmExtra;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.BackUpExtraClasses
{
    public class BackUpExtra : BackUp
    {
        private uint _restorePointExtraQuantity;

        public BackUpExtra(string name)
            : base(name)
        {
            _restorePointExtraQuantity = 1;
            RestorePointExtraList = new List<RestorePointExtra>();
        }

        [JsonConstructor]
        private BackUpExtra(string name, List<RestorePointExtra> restorePointExtraList = null)
            : base(name)
        {
            restorePointExtraList ??= new List<RestorePointExtra>();
            RestorePointExtraList = restorePointExtraList;
            _restorePointExtraQuantity = (uint)(restorePointExtraList.Count + 1);
        }

        [JsonIgnore]
        public ImmutableList<RestorePointExtra> ImmutableRestorePointExtraList =>
            RestorePointExtraList.ToImmutableList();
        [JsonProperty]
        private List<RestorePointExtra> RestorePointExtraList { get; set; }

        public RestorePointExtra MakeRestorePoint(JobObjects jobObjects, IRepositoryExtra repository, IStorageAlgorithmExtra algorithm)
        {
            var restorePoint = new RestorePointExtra(jobObjects, algorithm, _restorePointExtraQuantity, repository, Name);
            RestorePointExtraList.Add(restorePoint);
            _restorePointExtraQuantity++;
            return restorePoint;
        }

        public bool CanDeleteRestorePoint(RestorePointExtra restorePointExtra)
            => RestorePointExtraList.Contains(restorePointExtra);

        public void DeleteRestorePoint(RestorePointExtra restorePointExtra)
        {
            if (!RestorePointExtraList.Contains(restorePointExtra))
                throw new BackUpsExtraExceptions("restore point wasn't");
            RestorePointExtraList.Remove(restorePointExtra);
        }
    }
}