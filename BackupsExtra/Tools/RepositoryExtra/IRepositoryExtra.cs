using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.Repository;

namespace BackupsExtra.Tools.RepositoryExtra
{
    public interface IRepositoryExtra : IRepository
    {
        List<Storage> UnCompressingObjectsToOriginalLocation(Storage storage, string backUpName, string restorePointName, string compressedName);
        List<Storage> UnCompressingObjectsToDifferentLocation(Storage storage, string backUpName, string restorePointName, string compressedName);
    }
}