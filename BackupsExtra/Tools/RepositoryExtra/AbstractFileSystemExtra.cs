using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.Repository;

namespace BackupsExtra.Tools.RepositoryExtra
{
    public class AbstractFileSystemExtra : AbstractFileSystem, IRepositoryExtra
    {
        public List<Storage> UnCompressingObjectsToOriginalLocation(Storage storage, string backUpName, string restorePointName,
            string compressedName)
        {
            throw new System.NotImplementedException();
        }

        public List<Storage> UnCompressingObjectsToDifferentLocation(Storage storage, string backUpName, string restorePointName,
            string compressedName)
        {
            throw new System.NotImplementedException();
        }
    }
}