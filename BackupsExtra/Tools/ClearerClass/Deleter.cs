using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.ClearingAlgorithm;
using BackupsExtra.Tools.RepositoryExtra;

namespace BackupsExtra.Tools.ClearerClass
{
    public class Deleter : IClearer
    {
        public void ClearRestoresPointExtra(
            ISelectingAlgorithm selectingAlgorithm,
            BackUpExtra backUpExtra,
            IRepositoryExtra repositoryExtra,
            bool isSplitAlgorithm)
        {
            var allRestorePoints = backUpExtra.ImmutableRestorePointExtraList.ToList();
            List<RestorePointExtra> deletingRestorePointExtras = selectingAlgorithm.GetRestorePointExtrasForClearing(allRestorePoints);
            foreach (RestorePointExtra restorePointExtra in deletingRestorePointExtras)
            {
                if (!backUpExtra.CanDeleteRestorePoint(restorePointExtra))
                {
                    throw new BackUpsExtraExceptions("Restore Point can't be deleted.");
                }

                foreach (StorageExtra storage in restorePointExtra.ImmutableStorages)
                {
                    repositoryExtra.DeleteStorageExtraFromRepository(storage);
                }

                backUpExtra.DeleteRestorePoint(restorePointExtra);
            }
        }
    }
}