using System;
using System.Collections.Generic;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.ClearingAlgorithm.HybridClearingAlgorithm
{
    public class LeastOneSelectingAlgorithm : ISelectingAlgorithm
    {
        public LeastOneSelectingAlgorithm(uint quantityOfRestorePointExtra, DateTime lastDate)
        {
            QuantityOfRestorePointExtra = quantityOfRestorePointExtra;
            if (QuantityOfRestorePointExtra == 0) throw new BackUpsExtraExceptions("The algorithm wants to clear all restore points.");
            LastDate = lastDate;
        }

        [JsonProperty]
        private uint QuantityOfRestorePointExtra { get; }
        [JsonProperty]
        private DateTime LastDate { get; }
        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList)
        {
            var restorePointExtrasForClearing = new List<RestorePointExtra>();
            if (restorePointExtraList.Count <= QuantityOfRestorePointExtra) return restorePointExtrasForClearing;
            foreach (RestorePointExtra restorePointExtra in restorePointExtraList)
            {
                restorePointExtrasForClearing.Add(restorePointExtra);
                if (restorePointExtrasForClearing.Count == restorePointExtraList.Count - restorePointExtrasForClearing.Count || restorePointExtra.Time > LastDate) break;
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