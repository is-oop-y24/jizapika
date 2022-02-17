using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.ClearingAlgorithm;
using BackupsExtra.Tools.RepositoryExtra;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.ClearerClass
{
    public class Merger : IClearer
    {
        [JsonProperty]
        private IRepositoryExtra _repositoryExtra;

        public Merger(IRepositoryExtra repositoryExtra)
        {
            _repositoryExtra = repositoryExtra;
        }

        public void ClearRestoresPointExtra(
        ISelectingAlgorithm selectingAlgorithm,
        BackUpExtra backUpExtra,
        bool isSplitAlgorithm)
        {
            var allRestorePoints = backUpExtra.ImmutableRestorePointExtraList.ToList();
            List<RestorePointExtra> mergingRestorePointExtras = selectingAlgorithm.GetRestorePointExtrasForClearing(allRestorePoints);
            mergingRestorePointExtras.Add(selectingAlgorithm.GetFirstNotClearingRestorePoint(allRestorePoints));
            for (int index = 1; index < mergingRestorePointExtras.Count; index++)
            {
                _repositoryExtra.MergeTwoRestorePointExtras(mergingRestorePointExtras[index - 1], mergingRestorePointExtras[index], backUpExtra, isSplitAlgorithm);
            }
        }
    }
}