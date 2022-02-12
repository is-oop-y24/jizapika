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
        private uint _restorePointExtraQuantity;

        public BackUpExtra(string name)
            : base(name)
        {
            _restorePointExtraQuantity = 1;
            LinkedRestorePointExtraList = new LinkedList<RestorePointExtra>();
        }

        public LinkedList<RestorePointExtra> LinkedRestorePointExtraList { get; }

        public RestorePointExtra MakeRestorePointExtra(JobObjects jobObjects, IRepositoryExtra repositoryExtra, IStorageAlgorithmExtra algorithmExtra)
        {
            var restorePointExtra = new RestorePointExtra(jobObjects, algorithmExtra, _restorePointExtraQuantity, repositoryExtra, Name);
            LinkedRestorePointExtraList.AddLast(restorePointExtra);
            _restorePointExtraQuantity++;
            return restorePointExtra;
        }
    }
}