using System;
using System.Collections.Generic;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;

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

        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList)
        {
            var restorePointExtrasForClearing = new List<RestorePointExtra>();
            if (restorePointExtraList.Count <= _quantityOfRestorePoint) return restorePointExtrasForClearing;
            foreach (RestorePointExtra restorePointExtra in restorePointExtraList)
            {
                restorePointExtrasForClearing.Add(restorePointExtra);
                if (restorePointExtrasForClearing.Count == restorePointExtraList.Count - restorePointExtrasForClearing.Count || restorePointExtra.Time > _lastDate) break;
            }

            if (restorePointExtrasForClearing.Count == restorePointExtraList.Count)
                throw new BackUpsExtraExceptions("The algorithm wants to delete all restore points.");
            return restorePointExtrasForClearing;
        }
    }
}