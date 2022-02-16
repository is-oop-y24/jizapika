using System.Linq;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using Backups.Tools.StorageAlgorithm;

namespace Backups.Services
{
    public class BackUpJob
    {
        public BackUpJob(IRepository repository, IStorageAlgorithm algorithm, string backUpName)
        {
            Repository = repository;
            Algorithm = algorithm;
            JobObjects = new JobObjects();
            BackUp = new BackUp(backUpName);
        }

        protected BackUp BackUp { get; }
        protected IRepository Repository { get; }
        protected IStorageAlgorithm Algorithm { get; }
        protected JobObjects JobObjects { get; }

        public JobObject AddJobObject(string way)
        {
            return JobObjects.AddJobObject(way);
        }

        public bool DeleteJobObject(JobObject jobObject) => JobObjects.DeleteJobObject(jobObject);
        public RestorePoint MakeRestorePoint() => BackUp.MakeRestorePoint(JobObjects, Repository, Algorithm);
        public int QuantityOfRestorePoints() => BackUp.ImmutableRestorePointList.Count;

        public int QuantityOfStorages() => BackUp.ImmutableRestorePointList.Sum(restorePoint => restorePoint.ImmutableStorages.Count);
    }
}