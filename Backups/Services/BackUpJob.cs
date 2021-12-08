using System.Linq;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using Backups.Tools.StorageAlgorithm;

namespace Backups.Services
{
    public class BackUpJob
    {
        private IRepository _repository;
        private IStorageAlgorithm _algorithm;
        private JobObjects _jobObjects;
        private BackUp _backUp;

        public BackUpJob(IRepository repository, IStorageAlgorithm algorithm, string backUpName)
        {
            _repository = repository;
            _algorithm = algorithm;
            _jobObjects = new JobObjects();
            _backUp = new BackUp(backUpName);
        }

        public JobObject AddJobObject(string way)
        {
            return _jobObjects.AddJobObject(way);
        }

        public bool DeleteJobObject(JobObject jobObject) => _jobObjects.DeleteJobObject(jobObject);
        public RestorePoint MakeRestorePoint() => _backUp.MakeRestorePoint(_jobObjects, _repository, _algorithm);
        public int QuantityOfRestorePoints() => _backUp.RestorePointList.Count;

        public int QuantityOfStorages() => _backUp.RestorePointList.Sum(restorePoint => restorePoint.Storages.Count);
    }
}