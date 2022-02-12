using System.Collections.Generic;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.ClearingAlgorithm
{
    public class SelectingAlgorithmByNumberOfRestorePoints : ISelectingAlgorithm
    {
        private uint _quantityOfRestorePoint;

        public SelectingAlgorithmByNumberOfRestorePoints(uint quantityOfRestorePoint)
        {
            _quantityOfRestorePoint = quantityOfRestorePoint;
            if (_quantityOfRestorePoint == 0) throw new BackUpsExtraExceptions("The algorithm wants to delete all restore points.");
        }

        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList)
        {
            var restorePointExtrasForClearing = new List<RestorePointExtra>();
            if (restorePointExtraList.Count <= _quantityOfRestorePoint) return restorePointExtrasForClearing;
            foreach (RestorePointExtra restorePointExtra in restorePointExtraList)
            {
                restorePointExtrasForClearing.Add(restorePointExtra);
                if (restorePointExtrasForClearing.Count == restorePointExtraList.Count - restorePointExtrasForClearing.Count) break;
            }

            return restorePointExtrasForClearing;
        }
    }
}