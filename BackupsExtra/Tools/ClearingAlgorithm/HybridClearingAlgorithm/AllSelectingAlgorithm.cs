using System;
using System.Collections.Generic;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.ClearingAlgorithm.HybridClearingAlgorithm
{
    public class AllSelectingAlgorithm : ISelectingAlgorithm
    {
        private uint _quantityOfRestorePointExtra;
        private DateTime _lastDate;

        public AllSelectingAlgorithm(uint quantityOfRestorePointExtra, DateTime lastDate)
        {
            _quantityOfRestorePointExtra = quantityOfRestorePointExtra;
            _lastDate = lastDate;
        }

        public LinkedList<RestorePointExtra> GetRestorePointExtrasForClearing(LinkedList<RestorePointExtra> restorePointExtraList)
        {
            var clearingAlgorithmByNumberOfRestorePoints = new SelectingAlgorithmByNumberOfRestorePoints(_quantityOfRestorePointExtra);
            var clearingAlgorithmByDate = new SelectingAlgorithmByDate(_lastDate);
            LinkedList<RestorePointExtra> restorePointExtrasForClearingByNumberOfRestorePoints
                = clearingAlgorithmByNumberOfRestorePoints.GetRestorePointExtrasForClearing(restorePointExtraList);
            return clearingAlgorithmByDate.GetRestorePointExtrasForClearing(
                restorePointExtrasForClearingByNumberOfRestorePoints);
        }
    }
}