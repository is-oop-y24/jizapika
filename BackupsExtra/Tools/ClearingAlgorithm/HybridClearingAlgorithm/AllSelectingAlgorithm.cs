using System;
using System.Collections.Generic;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.ClearingAlgorithm.HybridClearingAlgorithm
{
    public class AllSelectingAlgorithm : ISelectingAlgorithm
    {
        [JsonProperty]
        private uint _quantityOfRestorePointExtra;
        [JsonProperty]
        private DateTime _lastDate;

        public AllSelectingAlgorithm(uint quantityOfRestorePointExtra, DateTime lastDate)
        {
            _quantityOfRestorePointExtra = quantityOfRestorePointExtra;
            _lastDate = lastDate;
        }

        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList)
        {
            var clearingAlgorithmByNumberOfRestorePoints = new SelectingAlgorithmByNumberOfRestorePoints(_quantityOfRestorePointExtra);
            var clearingAlgorithmByDate = new SelectingAlgorithmByDate(_lastDate);
            List<RestorePointExtra> restorePointExtrasForClearingByNumberOfRestorePoints
                = clearingAlgorithmByNumberOfRestorePoints.GetRestorePointExtrasForClearing(restorePointExtraList);
            return clearingAlgorithmByDate.GetRestorePointExtrasForClearing(
                restorePointExtrasForClearingByNumberOfRestorePoints);
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