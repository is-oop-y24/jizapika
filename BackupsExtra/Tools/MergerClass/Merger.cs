using Backups.Tools.BackUpClasses;
using BackupsExtra.Tools.BackUpExtraClasses;
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

        public void MergeTwoRestorePoints(RestorePointExtra oldRestorePointExtra, RestorePointExtra newRestorePointExtra)
        {
            foreach (StorageExtra oldStorageExtra in oldRestorePointExtra.Storages)
            {
                foreach (StorageExtra newStorageExtra in newRestorePointExtra.Storages)
                {
                    if (oldStorageExtra.GetId() == newStorageExtra.GetId())
                    {
                        _repositoryExtra.DeleteStorageExtra(oldStorageExtra);
                    }
                }
            }
        }
    }
}