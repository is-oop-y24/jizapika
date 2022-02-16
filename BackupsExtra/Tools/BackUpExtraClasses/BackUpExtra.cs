using System.Collections.Generic;
using System.Collections.Immutable;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using Backups.Tools.StorageAlgorithm;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.RepositoryExtra;
using BackupsExtra.Tools.StorageAlgorithmExtra;

namespace BackupsExtra.Tools.BackUpExtraClasses
{
    public class BackUpExtra : BackUp
    {
        private uint _restorePointExtraQuantity;
        private List<RestorePointExtra> _restorePointExtraList;

        public BackUpExtra(string name)
            : base(name)
        {
            _restorePointExtraQuantity = 1;
            _restorePointExtraList = new List<RestorePointExtra>();
        }

        public ImmutableList<RestorePointExtra> ImmutableRestorePointExtraList =>
            _restorePointExtraList.ToImmutableList();

        public RestorePointExtra MakeRestorePointExtra(JobObjects jobObjects, IRepositoryExtra repositoryExtra, IStorageAlgorithmExtra algorithmExtra)
        {
            var restorePointExtra = new RestorePointExtra(jobObjects, algorithmExtra, _restorePointExtraQuantity, repositoryExtra, Name);
            _restorePointExtraList.Add(restorePointExtra);
            _restorePointExtraQuantity++;
            return restorePointExtra;
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