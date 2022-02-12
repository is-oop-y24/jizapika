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
            LinkedList<RestorePointExtra> selectingRestorePointExtras = selectingAlgorithm.GetRestorePointExtrasForClearing(backUpExtra.LinkedRestorePointExtraList);
            
        }
    }
}