using System;
using System.Collections.Generic;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.ClearingAlgorithm.HybridClearingAlgorithm
{
    public class AllSelectingAlgorithm : ISelectingAlgorithm
    {
        public AllSelectingAlgorithm(uint quantityOfRestorePointExtra, DateTime lastDate)
        {
            QuantityOfRestorePointExtra = quantityOfRestorePointExtra;
            LastDate = lastDate;
        }

        [JsonProperty]
        private uint QuantityOfRestorePointExtra { get; }
        [JsonProperty]
        private DateTime LastDate { get; }
        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList)
        {
            var clearingAlgorithmByNumberOfRestorePoints = new SelectingAlgorithmByNumberOfRestorePoints(QuantityOfRestorePointExtra);
            var clearingAlgorithmByDate = new SelectingAlgorithmByDate(LastDate);
            List<RestorePointExtra> restorePointExtrasForClearingByNumberOfRestorePoints
                = clearingAlgorithmByNumberOfRestorePoints.GetRestorePointExtrasForClearing(restorePointExtraList);
            return clearingAlgorithmByDate.GetRestorePointExtrasForClearing(
                restorePointExtrasForClearingByNumberOfRestorePoints);
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