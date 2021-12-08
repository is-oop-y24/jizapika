using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;

namespace Backups.Tools.Repository
{
    public interface IRepository
    {
        Storage CompressingObjects(List<Storage> storages, string backUpName, string restorePointName);
        Storage CopyObject(JobObject jobObject);
    }
}