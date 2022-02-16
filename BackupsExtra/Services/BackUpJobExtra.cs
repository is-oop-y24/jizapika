using Backups.Services;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using Backups.Tools.StorageAlgorithm;
using BackupsExtra.Tools.ClearerClass;
using BackupsExtra.Tools.ClearingAlgorithm;

namespace BackupsExtra.Services
{
    public class BackUpJobExtra : BackUpJob
    {
        private ISelectingAlgorithm _selectingAlgorithm;
        private JobObjects _jobObjects;
        private IClearer _clearer;
        public BackUpJobExtra(IRepository repository, IStorageAlgorithm storageAlgorithm, ISelectingAlgorithm selectingAlgorithm, string backUpName, IClearer clearer)
            : base(repository, storageAlgorithm, backUpName)
        {
            _selectingAlgorithm = selectingAlgorithm;
            _jobObjects = new JobObjects();
            _clearer = clearer;
        }
    }
}