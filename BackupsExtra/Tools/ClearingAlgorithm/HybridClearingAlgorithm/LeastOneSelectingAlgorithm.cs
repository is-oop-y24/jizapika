using System;
using System.Collections.Generic;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.ClearingAlgorithm.HybridClearingAlgorithm
{
    public class LeastOneSelectingAlgorithm : ISelectingAlgorithm
    {
        [JsonProperty]
        private uint _quantityOfRestorePointExtra;
        [JsonProperty]
        private DateTime _lastDate;

        public LeastOneSelectingAlgorithm(uint quantityOfRestorePointExtra, DateTime lastDate)
        {
            _quantityOfRestorePointExtra = quantityOfRestorePointExtra;
            if (_quantityOfRestorePointExtra == 0) throw new BackUpsExtraExceptions("The algorithm wants to clear all restore points.");
            _lastDate = lastDate;
        }

        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList)
        {
            var restorePointExtrasForClearing = new List<RestorePointExtra>();
            if (restorePointExtraList.Count <= _quantityOfRestorePointExtra) return restorePointExtrasForClearing;
            foreach (RestorePointExtra restorePointExtra in restorePointExtraList)
            {
                restorePointExtrasForClearing.Add(restorePointExtra);
                if (restorePointExtrasForClearing.Count == restorePointExtraList.Count - restorePointExtrasForClearing.Count || restorePointExtra.Time > _lastDate) break;
            }

            if (restorePointExtrasForClearing.Count == restorePointExtraList.Count)
                throw new BackUpsExtraExceptions("The algorithm wants to clear all restore points.");
            return restorePointExtrasForClearing;
        }

        public RestorePointExtra GetFirstNotClearingRestorePoint(List<RestorePointExtra> restorePointExtraList)
        {
            List<RestorePointExtra> restorePoints = GetRestorePointExtrasForClearing(restorePointExtraList);
            foreach (RestorePointExtra restorePoint in restorePoints)
            {
                if (!restorePoints.Contains(restorePoint)) return restorePoint;
            }

            throw new BackUpsExtraExceptions("The algorithm wants to clear all restore points.");
        }
    }
}