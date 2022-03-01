using System;
using System.Collections.Generic;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.SelectingAlgorithm.HybridSelectingAlgorithm
{
    public class AllSelectingAlgorithm : ISelectingAlgorithm
    {
        public AllSelectingAlgorithm(List<ISelectingAlgorithm> algorithms)
        {
            Algorithms = algorithms;
        }

        [JsonProperty]
        private List<ISelectingAlgorithm> Algorithms { get; }
        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList)
        {
            foreach (ISelectingAlgorithm algorithm in Algorithms)
            {
                restorePointExtraList = algorithm.GetRestorePointExtrasForClearing(restorePointExtraList);
            }

            return restorePointExtraList;
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