using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.RepositoryExtra
{
    public interface IRepositoryExtra : IRepository
    {
        List<Storage> UnCompressingObjectsToOriginalLocation(
            Storage storage, string backUpName, string restorePointName, string compressedName);
        List<Storage> UnCompressingObjectsToDifferentLocation(
            Storage storage, string backUpName, string restorePointName, string compressedName);

        void DeleteJobObject(JobObject jobObject);
        void DeleteStorageExtra(StorageExtra storageExtra);
    }
}