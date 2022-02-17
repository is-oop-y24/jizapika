using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.ClearingAlgorithm;
using BackupsExtra.Tools.RepositoryExtra;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.ClearerClass
{
    public class Deleter : IClearer
    {
        [JsonProperty]
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
                foreach (StorageExtra storage in restorePointExtra.ImmutableStorages)
                {
                    _repositoryExtra.DeleteStorageExtraFromRepository(storage);
                }

                if (!backUpExtra.CanDeleteRestorePoint(restorePointExtra))
                    throw new BackUpsExtraExceptions("Restore Point can't be deleted.");
                backUpExtra.DeleteRestorePoint(restorePointExtra);
            }
        }
    }
}