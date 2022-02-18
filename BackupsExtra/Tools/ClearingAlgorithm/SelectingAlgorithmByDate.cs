using System;
using System.Collections.Generic;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.ClearingAlgorithm
{
    public class SelectingAlgorithmByDate : ISelectingAlgorithm
    {
        public SelectingAlgorithmByDate(DateTime lastDate)
        {
            LastDate = lastDate;
        }

        [JsonProperty]
        private DateTime LastDate { get; }
        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList)
        {
            var restorePointExtrasForClearing = new List<RestorePointExtra>();
            foreach (RestorePointExtra restorePointExtra in restorePointExtraList)
            {
                if (restorePointExtra.Time > LastDate) break;
                restorePointExtrasForClearing.Add(restorePointExtra);
            }

            if (restorePointExtrasForClearing.Count == restorePointExtraList.Count)
                throw new BackUpsExtraExceptions("The algorithm wants to clear all restore points.");
            return restorePointExtrasForClearing;
        }

        public RestorePointExtra GetFirstNotClearingRestorePoint(List<RestorePointExtra> restorePointExtraList)
        {
            List<RestorePointExtra> restorePoints = GetRestorePointExtrasForClearing(restorePointExtraList);
            foreach (RestorePointExtra restorePoint in restorePointExtraList)
            {
                if (!restorePoints.Contains(restorePoint)) return restorePoint;
            }

            throw new BackUpsExtraExceptions("The algorithm wants to clear all restore points.");
        }
    }
}