using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using BackupsExtra.Exceptions;

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

        public List<RestorePoint> GetRestorePointsForClearing(List<RestorePoint> restorePointList)
        {
            var restorePointsForClearing = new List<RestorePoint>();
            if (restorePointList.Count <= _quantityOfRestorePoint) return restorePointsForClearing;
            foreach (RestorePoint restorePoint in restorePointList)
            {
                restorePointsForClearing.Add(restorePoint);
                if (restorePointsForClearing.Count == restorePointList.Count - restorePointsForClearing.Count) break;
            }

            return restorePointsForClearing;
        }
    }
}