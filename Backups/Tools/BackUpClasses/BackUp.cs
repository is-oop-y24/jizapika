using System.Collections.Generic;
using System.Collections.Immutable;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using Backups.Tools.StorageAlgorithm;
using Newtonsoft.Json;

namespace Backups.Tools.BackUpClasses
{
    public class BackUp
    {
        private uint _restorePointQuantity;
        private List<RestorePoint> _restorePointList;

        public BackUp(string name)
        {
            _restorePointQuantity = 1;
            _restorePointList = new List<RestorePoint>();
            Name = name;
        }

        [JsonIgnore]
        public ImmutableList<RestorePoint> ImmutableRestorePointList => _restorePointList.ToImmutableList();
        public string Name { get; }

        public RestorePoint MakeRestorePoint(JobObjects jobObjects, IRepository repository, IStorageAlgorithm algorithm)
        {
            var restorePoint = new RestorePoint(jobObjects, algorithm, _restorePointQuantity, repository, Name);
            _restorePointList.Add(restorePoint);
            _restorePointQuantity++;
            return restorePoint;
        }
    }
}