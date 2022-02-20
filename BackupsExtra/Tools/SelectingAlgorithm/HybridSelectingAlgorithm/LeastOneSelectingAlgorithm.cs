using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.SelectingAlgorithm.HybridSelectingAlgorithm
{
    public class LeastOneSelectingAlgorithm : ISelectingAlgorithm
    {
        public LeastOneSelectingAlgorithm(List<ISelectingAlgorithm> algorithms)
        {
            Algorithms = algorithms;
        }

        [JsonProperty]
        private List<ISelectingAlgorithm> Algorithms { get; }

        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList)
        {
            var finalRestorePointExtraList = new List<RestorePointExtra>();
            var restorePointExtraListForEachAlgorithm = new List<List<RestorePointExtra>>();
            foreach (ISelectingAlgorithm algorithm in Algorithms)
            {
                restorePointExtraListForEachAlgorithm.Add(
                    algorithm.GetRestorePointExtrasForClearing(restorePointExtraList));
            }

            foreach (RestorePointExtra concreteRestorePointExtra in restorePointExtraList)
            {
                if (restorePointExtraListForEachAlgorithm.Any(restorePointExtraListForAlgorithm => restorePointExtraListForAlgorithm.Contains(concreteRestorePointExtra)))
                    finalRestorePointExtraList.Add(concreteRestorePointExtra);
            }

            return finalRestorePointExtraList;
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