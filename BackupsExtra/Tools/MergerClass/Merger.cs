using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.ClearingAlgorithm;
using BackupsExtra.Tools.RepositoryExtra;

namespace BackupsExtra.Tools.MergerClass
{
    public class Merger
    {
        private IRepositoryExtra _repositoryExtra;

        public Merger(IRepositoryExtra repositoryExtra)
        {
            _repositoryExtra = repositoryExtra;
        }

        public void MergeRestoresPointExtra(
        ISelectingAlgorithm selectingAlgorithm,
        BackUpExtra backUpExtra,
        string backUpExtraName,
        string compressedName,
        bool isSplitAlgorithm)
        {
            var allRestorePoints = backUpExtra.ImmutableRestorePointExtraList.ToList();
            List<RestorePointExtra> mergingRestorePointExtras = selectingAlgorithm.GetRestorePointExtrasForClearing(allRestorePoints);
            mergingRestorePointExtras.Add(selectingAlgorithm.GetFirstNotClearingRestorePoint(allRestorePoints));
            for (int index = 1; index < mergingRestorePointExtras.Count; index++)
            {
                _repositoryExtra.MergeTwoRestorePointExtras(mergingRestorePointExtras[index - 1], mergingRestorePointExtras[index], backUpExtra, backUpExtraName, mergingRestorePointExtras[index].RestorePointName, compressedName, isSplitAlgorithm);
            }
        }
    }
}