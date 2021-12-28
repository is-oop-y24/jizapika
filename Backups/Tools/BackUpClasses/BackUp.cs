using System.Collections.Generic;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using Backups.Tools.StorageAlgorithm;

namespace Backups.Tools.BackUpClasses
{
    public class BackUp
    {
        private uint _restorePointQuantity;

        public BackUp(string name)
        {
            _restorePointQuantity = 1;
            RestorePointList = new List<RestorePoint>();
            Name = name;
        }

        public List<RestorePoint> RestorePointList { get; }
        public string Name { get; }

        public RestorePoint MakeRestorePoint(JobObjects jobObjects, IRepository repository, IStorageAlgorithm algorithm)
        {
            var restorePoint = new RestorePoint(jobObjects, algorithm, _restorePointQuantity, repository, Name);
            RestorePointList.Add(restorePoint);
            _restorePointQuantity++;
            return restorePoint;
        }
    }
}