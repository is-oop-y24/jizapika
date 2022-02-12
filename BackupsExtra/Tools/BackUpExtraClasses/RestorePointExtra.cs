using System;
using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using BackupsExtra.Tools.RepositoryExtra;
using BackupsExtra.Tools.StorageAlgorithmExtra;

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
            string restorePointName = "RestorePoint" + Id;
            StoragesExtra = algorithmExtra.GetStorages(jobObjects, repositoryExtra, backUpName, restorePointName);
            Time = DateTime.UtcNow;
        }

        public List<StorageExtra> StoragesExtra { get; }
    }
}