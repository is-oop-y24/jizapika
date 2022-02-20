using System.Collections.Generic;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.SelectingAlgorithm
{
    public class SelectingAlgorithmByNumberOfRestorePoints : ISelectingAlgorithm
    {
        public SelectingAlgorithmByNumberOfRestorePoints(uint quantityOfRestorePoint)
        {
            QuantityOfRestorePoint = quantityOfRestorePoint;
            if (QuantityOfRestorePoint == 0) throw new BackUpsExtraExceptions("The algorithm wants to clear all restore points.");
        }

        [JsonProperty]
        private uint QuantityOfRestorePoint { get; }
        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList)
        {
            var restorePointExtrasForClearing = new List<RestorePointExtra>();
            if (restorePointExtraList.Count <= QuantityOfRestorePoint) return restorePointExtrasForClearing;
            foreach (RestorePointExtra restorePointExtra in restorePointExtraList)
            {
                restorePointExtrasForClearing.Add(restorePointExtra);
                if (restorePointExtrasForClearing.Count == restorePointExtraList.Count - restorePointExtrasForClearing.Count) break;
            }

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