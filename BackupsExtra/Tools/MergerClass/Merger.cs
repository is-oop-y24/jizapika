using System.Collections.Generic;
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

        public void MergeRestorePointExtras(ISelectingAlgorithm selectingAlgorithm, BackUpExtra backUpExtra)
        {
            List<RestorePointExtra> selectingRestorePointExtras = selectingAlgorithm.GetRestorePointExtrasForClearing(backUpExtra.RestorePointExtraList);
            for (int index = 1; index < selectingRestorePointExtras.Count; index++)
            {
                _repositoryExtra.MergeTwoRestorePointExtras(
                    selectingRestorePointExtras[index - 1],
                    selectingRestorePointExtras[index]);
            }
        }
    }
}