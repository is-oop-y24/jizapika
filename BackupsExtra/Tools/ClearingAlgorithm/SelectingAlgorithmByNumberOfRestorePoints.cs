using System.Collections.Generic;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.ClearingAlgorithm
{
    public class SelectingAlgorithmByNumberOfRestorePoints : ISelectingAlgorithm
    {
        [JsonProperty]
        private uint _quantityOfRestorePointExtra;

        public SelectingAlgorithmByNumberOfRestorePoints(uint quantityOfRestorePoint)
        {
            _quantityOfRestorePointExtra = quantityOfRestorePoint;
            if (_quantityOfRestorePointExtra == 0) throw new BackUpsExtraExceptions("The algorithm wants to clear all restore points.");
        }

        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList)
        {
            var restorePointExtrasForClearing = new List<RestorePointExtra>();
            if (restorePointExtraList.Count <= _quantityOfRestorePointExtra) return restorePointExtrasForClearing;
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
            foreach (RestorePointExtra restorePoint in restorePoints)
            {
                if (!restorePoints.Contains(restorePoint)) return restorePoint;
            }

            throw new BackUpsExtraExceptions("The algorithm wants to clear all restore points.");
        }
    }
}