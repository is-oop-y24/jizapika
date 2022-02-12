using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.RepositoryExtra
{
    public class FileSystemExtra : FileSystem, IRepositoryExtra
    {
        public FileSystemExtra(string root)
            : base(root)
        {
        }

        public List<Storage> UnCompressingObjectsToOriginalLocation(
            Storage storage,
            string backUpName,
            string restorePointName,
            string compressedName)
        {
            throw new System.NotImplementedException();
        }

        public List<Storage> UnCompressingObjectsToDifferentLocation(
            Storage storage,
            string backUpName,
            string restorePointName,
            string compressedName)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteJobObject(JobObject jobObject)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteStorageExtra(StorageExtra storageExtra)
        {
            throw new System.NotImplementedException();
        }
    }
}