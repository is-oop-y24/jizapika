using Backups.Services;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using Backups.Tools.StorageAlgorithm;
using BackupsExtra.Tools.ClearingAlgorithm;

namespace BackupsExtra.Services
{
    public class BackUpJobExtra : BackUpJob
    {
        private IClearingAlgorithm _clearingAlgorithm;
        private JobObjects _jobObjects;
        public BackUpJobExtra(IRepository repository, IStorageAlgorithm storageAlgorithm, IClearingAlgorithm clearingAlgorithm, string backUpName)
            : base(repository, storageAlgorithm, backUpName)
        {
            _clearingAlgorithm = clearingAlgorithm;
            _jobObjects = new JobObjects();
        }
    }
}