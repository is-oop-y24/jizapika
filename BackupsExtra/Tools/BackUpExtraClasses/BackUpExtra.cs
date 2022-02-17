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
        [JsonProperty]
        private uint _restorePointExtraQuantity;
        [JsonProperty]
        private List<RestorePointExtra> _restorePointExtraList;

        public BackUpExtra(string name)
            : base(name)
        {
            _restorePointExtraQuantity = 1;
            _restorePointExtraList = new List<RestorePointExtra>();
        }

        public ImmutableList<RestorePointExtra> ImmutableRestorePointExtraList =>
            _restorePointExtraList.ToImmutableList();

        public RestorePointExtra MakeRestorePoint(JobObjects jobObjects, IRepositoryExtra repository, IStorageAlgorithmExtra algorithm)
        {
            var restorePoint = new RestorePointExtra(jobObjects, algorithm, _restorePointExtraQuantity, repository, Name);
            _restorePointExtraList.Add(restorePoint);
            _restorePointExtraQuantity++;
            return restorePoint;
        }

        public bool CanDeleteRestorePoint(RestorePointExtra restorePointExtra)
            => _restorePointExtraList.Contains(restorePointExtra);

        public void DeleteRestorePoint(RestorePointExtra restorePointExtra)
        {
            if (!_restorePointExtraList.Contains(restorePointExtra))
                throw new BackUpsExtraExceptions("restore point wasn't");
            _restorePointExtraList.Remove(restorePointExtra);
        }
    }
}