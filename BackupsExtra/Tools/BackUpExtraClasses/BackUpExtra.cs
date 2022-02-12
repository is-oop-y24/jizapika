using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using Backups.Tools.StorageAlgorithm;
using BackupsExtra.Tools.RepositoryExtra;
using BackupsExtra.Tools.StorageAlgorithmExtra;

namespace BackupsExtra.Tools.BackUpExtraClasses
{
    public class BackUpExtra : BackUp
    {
        public uint _restorePointExtraQuantity;

        public BackUpExtra(string name)
            : base(name)
        {
            _restorePointExtraQuantity = 1;
            RestorePointExtraList = new List<RestorePointExtra>();
        }

        public List<RestorePointExtra> RestorePointExtraList { get; }

        public RestorePointExtra MakeRestorePointExtra(JobObjects jobObjects, IRepositoryExtra repositoryExtra, IStorageAlgorithmExtra algorithmExtra)
        {
            var restorePointExtra = new RestorePointExtra(jobObjects, algorithmExtra, _restorePointExtraQuantity, repositoryExtra, Name);
            RestorePointExtraList.Add(restorePointExtra);
            _restorePointExtraQuantity++;
            return restorePointExtra;
        }
    }
}