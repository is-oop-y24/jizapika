using System;
using System.Collections.Generic;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.ClearingAlgorithm.HybridClearingAlgorithm
{
    public class AllSelectingAlgorithm : ISelectingAlgorithm
    {
        private uint _quantityOfRestorePoint;
        private DateTime _lastDate;

        public AllSelectingAlgorithm(uint quantityOfRestorePoint, DateTime lastDate)
        {
            _quantityOfRestorePoint = quantityOfRestorePoint;
            _lastDate = lastDate;
        }

        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList)
        {
            var clearingAlgorithmByNumberOfRestorePoints = new SelectingAlgorithmByNumberOfRestorePoints(_quantityOfRestorePoint);
            var clearingAlgorithmByDate = new SelectingAlgorithmByDate(_lastDate);
            List<RestorePointExtra> restorePointExtrasForClearingByNumberOfRestorePoints
                = clearingAlgorithmByNumberOfRestorePoints.GetRestorePointExtrasForClearing(restorePointExtraList);
            return clearingAlgorithmByDate.GetRestorePointExtrasForClearing(
                restorePointExtrasForClearingByNumberOfRestorePoints);
        }
    }
}