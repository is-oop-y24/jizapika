using System;
using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.ClearingAlgorithm
{
    public class SelectingAlgorithmByDate : ISelectingAlgorithm
    {
        private DateTime _lastDate;

        public SelectingAlgorithmByDate(DateTime lastDate)
        {
            _lastDate = lastDate;
        }

        public LinkedList<RestorePointExtra> GetRestorePointExtrasForClearing(LinkedList<RestorePointExtra> restorePointExtraList)
        {
            var restorePointExtrasForClearing = new LinkedList<RestorePointExtra>();
            foreach (RestorePointExtra restorePointExtra in restorePointExtraList)
            {
                if (restorePointExtra.Time > _lastDate) break;
                restorePointExtrasForClearing.AddLast(restorePointExtra);
            }

            if (restorePointExtrasForClearing.Count == restorePointExtraList.Count)
                throw new BackUpsExtraExceptions("The algorithm wants to delete all restore points.");
            return restorePointExtrasForClearing;
        }
    }
}