using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;

namespace Backups.Tools.StorageAlgorithm
{
    public interface IStorageAlgorithm
    {
        public List<Storage> GetStorages(
            JobObjects sourceRepository, IRepository repository, string backUpName, string restorePointName);
    }
}