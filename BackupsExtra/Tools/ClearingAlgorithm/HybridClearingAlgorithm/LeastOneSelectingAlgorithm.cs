using System;
using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using BackupsExtra.Exceptions;

namespace BackupsExtra.Tools.ClearingAlgorithm.HybridClearingAlgorithm
{
    public class LeastOneSelectingAlgorithm : ISelectingAlgorithm
    {
        private uint _quantityOfRestorePoint;
        private DateTime _lastDate;

        public LeastOneSelectingAlgorithm(uint quantityOfRestorePoint, DateTime lastDate)
        {
            _quantityOfRestorePoint = quantityOfRestorePoint;
            if (_quantityOfRestorePoint == 0) throw new BackUpsExtraExceptions("The algorithm wants to delete all restore points.");
            _lastDate = lastDate;
        }

        public List<RestorePoint> GetRestorePointsForClearing(List<RestorePoint> restorePointList)
        {
            var restorePointsForClearing = new List<RestorePoint>();
            if (restorePointList.Count <= _quantityOfRestorePoint) return restorePointsForClearing;
            foreach (RestorePoint restorePoint in restorePointList)
            {
                restorePointsForClearing.Add(restorePoint);
                if (restorePointsForClearing.Count == restorePointList.Count - restorePointsForClearing.Count || restorePoint.Time > _lastDate) break;
            }

            if (restorePointsForClearing.Count == restorePointList.Count)
                throw new BackUpsExtraExceptions("The algorithm wants to delete all restore points.");
            return restorePointsForClearing;
        }
    }
}