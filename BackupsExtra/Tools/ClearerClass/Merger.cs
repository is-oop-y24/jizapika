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
        public void ClearRestoresPointExtra(
        ISelectingAlgorithm selectingAlgorithm,
        BackUpExtra backUpExtra,
        IRepositoryExtra repositoryExtra,
        bool isSplitAlgorithm)
        {
            var allRestorePoints = backUpExtra.ImmutableRestorePointExtraList.ToList();
            List<RestorePointExtra> mergingRestorePointExtras = selectingAlgorithm.GetRestorePointExtrasForClearing(allRestorePoints);
            mergingRestorePointExtras.Add(selectingAlgorithm.GetFirstNotClearingRestorePoint(allRestorePoints));
            for (int index = 1; index < mergingRestorePointExtras.Count; index++)
            {
                repositoryExtra.MergeTwoRestorePointExtras(
                    mergingRestorePointExtras[index - 1],
                    mergingRestorePointExtras[index],
                    backUpExtra,
                    isSplitAlgorithm);
            }
        }
    }
}