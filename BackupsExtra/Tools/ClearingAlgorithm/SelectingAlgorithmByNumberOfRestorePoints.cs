using System.Collections.Generic;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.ClearingAlgorithm
{
    public class SelectingAlgorithmByNumberOfRestorePoints : ISelectingAlgorithm
    {
        private uint _quantityOfRestorePointExtra;

        public SelectingAlgorithmByNumberOfRestorePoints(uint quantityOfRestorePoint)
        {
            _quantityOfRestorePointExtra = quantityOfRestorePoint;
            if (_quantityOfRestorePointExtra == 0) throw new BackUpsExtraExceptions("The algorithm wants to delete all restore points.");
        }

        public LinkedList<RestorePointExtra> GetRestorePointExtrasForClearing(LinkedList<RestorePointExtra> restorePointExtraList)
        {
            var restorePointExtrasForClearing = new LinkedList<RestorePointExtra>();
            if (restorePointExtraList.Count <= _quantityOfRestorePointExtra) return restorePointExtrasForClearing;
            foreach (RestorePointExtra restorePointExtra in restorePointExtraList)
            {
                restorePointExtrasForClearing.AddLast(restorePointExtra);
                if (restorePointExtrasForClearing.Count == restorePointExtraList.Count - restorePointExtrasForClearing.Count) break;
            }

            return restorePointExtrasForClearing;
        }
    }
}