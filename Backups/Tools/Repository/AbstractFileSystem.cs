using System.Collections.Generic;
using Backups.Tools.BackUpClasses;
using Backups.Tools.JobObjectsClasses;

namespace Backups.Tools.Repository
{
    public class AbstractFileSystem : IRepository
    {
        public Storage CompressingObjects(List<Storage> storages, string backUpName, string restorePointName, string compressedName)
            => new Storage(string.Empty, true);

        public Storage CopyObject(JobObject jobObject) => new Storage(jobObject.Way, false);

        public string ObjectNameWithoutExtension(string way) => string.Empty;
    }
}