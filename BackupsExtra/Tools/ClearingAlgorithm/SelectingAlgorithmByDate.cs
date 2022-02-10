using System;
using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using BackupsExtra.Exceptions;

namespace BackupsExtra.Tools.ClearingAlgorithm
{
    public class SelectingAlgorithmByDate : ISelectingAlgorithm
    {
        private DateTime _lastDate;

        public SelectingAlgorithmByDate(DateTime lastDate)
        {
            _lastDate = lastDate;
        }

        public List<RestorePoint> GetRestorePointsForClearing(List<RestorePoint> restorePointList)
        {
            var restorePointsForClearing = new List<RestorePoint>();
            foreach (RestorePoint restorePoint in restorePointList)
            {
                if (restorePoint.Time > _lastDate) break;
                restorePointsForClearing.Add(restorePoint);
            }

            if (restorePointsForClearing.Count == restorePointList.Count)
                throw new BackUpsExtraExceptions("The algorithm wants to delete all restore points.");
            return restorePointsForClearing;
        }
    }
}