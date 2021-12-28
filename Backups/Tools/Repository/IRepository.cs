using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;

namespace Backups.Tools.Repository
{
    public interface IRepository
    {
        Storage CompressingObjects(List<Storage> storages, string backUpName, string restorePointName, string compressedName);
        Storage CopyObject(JobObject jobObject);
        public string ObjectNameWithoutExtension(string way);
    }
}