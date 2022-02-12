using System;
using System.Collections.Generic;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.ClearingAlgorithm.HybridClearingAlgorithm
{
    public class LeastOneSelectingAlgorithm : ISelectingAlgorithm
    {
        private uint _quantityOfRestorePointExtra;
        private DateTime _lastDate;

        public LeastOneSelectingAlgorithm(uint quantityOfRestorePointExtra, DateTime lastDate)
        {
            _quantityOfRestorePointExtra = quantityOfRestorePointExtra;
            if (_quantityOfRestorePointExtra == 0) throw new BackUpsExtraExceptions("The algorithm wants to delete all restore points.");
            _lastDate = lastDate;
        }

        public LinkedList<RestorePointExtra> GetRestorePointExtrasForClearing(LinkedList<RestorePointExtra> restorePointExtraList)
        {
            var restorePointExtrasForClearing = new LinkedList<RestorePointExtra>();
            if (restorePointExtraList.Count <= _quantityOfRestorePointExtra) return restorePointExtrasForClearing;
            foreach (RestorePointExtra restorePointExtra in restorePointExtraList)
            {
                restorePointExtrasForClearing.AddLast(restorePointExtra);
                if (restorePointExtrasForClearing.Count == restorePointExtraList.Count - restorePointExtrasForClearing.Count || restorePointExtra.Time > _lastDate) break;
            }

            if (restorePointExtrasForClearing.Count == restorePointExtraList.Count)
                throw new BackUpsExtraExceptions("The algorithm wants to delete all restore points.");
            return restorePointExtrasForClearing;
        }
    }
}