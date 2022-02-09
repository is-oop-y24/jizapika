using System;
using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using BackupsExtra.Exceptions;

namespace BackupsExtra.Tools.ClearingAlgorithm.HybridClearingAlgorithm
{
    public class AllClearingAlgorithm : IClearingAlgorithm
    {
        private uint _quantityOfRestorePoint;
        private DateTime _lastDate;

        public AllClearingAlgorithm(uint quantityOfRestorePoint, DateTime lastDate)
        {
            _quantityOfRestorePoint = quantityOfRestorePoint;
            _lastDate = lastDate;
        }

        public List<RestorePoint> GetRestorePointsForClearing(List<RestorePoint> restorePointList)
        {
            var clearingAlgorithmByNumberOfRestorePoints = new ClearingAlgorithmByNumberOfRestorePoints(_quantityOfRestorePoint);
            var clearingAlgorithmByDate = new ClearingAlgorithmByDate(_lastDate);
            List<RestorePoint> restorePointsForClearingByNumberOfRestorePoints = clearingAlgorithmByNumberOfRestorePoints.GetRestorePointsForClearing(restorePointList);
            return clearingAlgorithmByDate.GetRestorePointsForClearing(restorePointsForClearingByNumberOfRestorePoints);
        }
    }
}