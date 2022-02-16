using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.ClearingAlgorithm;
using BackupsExtra.Tools.RepositoryExtra;

namespace BackupsExtra.Tools.ClearerClass
{
    public class Deleter : IClearer
    {
        private IRepositoryExtra _repositoryExtra;

        public Deleter(IRepositoryExtra repositoryExtra)
        {
            _repositoryExtra = repositoryExtra;
        }

        public void ClearRestoresPointExtra(ISelectingAlgorithm selectingAlgorithm, BackUpExtra backUpExtra, bool isSplitAlgorithm)
        {
            var allRestorePoints = backUpExtra.ImmutableRestorePointExtraList.ToList();
            List<RestorePointExtra> deletingRestorePointExtras = selectingAlgorithm.GetRestorePointExtrasForClearing(allRestorePoints);
            foreach (RestorePointExtra restorePointExtra in deletingRestorePointExtras)
            {
                foreach (StorageExtra storage in restorePointExtra.StoragesExtra)
                {
                    _repositoryExtra.DeleteStorageExtraFromRepository(storage);
                }

                backUpExtra.DeleteRestorePoint(restorePointExtra);
            }
        }
    }
}