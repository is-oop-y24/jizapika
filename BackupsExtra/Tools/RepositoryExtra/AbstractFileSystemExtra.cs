using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Tools.Repository;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.RepositoryExtra
{
    public class AbstractFileSystemExtra : AbstractFileSystem, IRepositoryExtra
    {
        public List<StorageExtra> UnCompressingObjectsToOriginalLocation(StorageExtra storageExtra)
            => storageExtra.ImmutableOriginalWays.Select(originalWay =>
                    storageExtra.CanGetId()
                        ? new StorageExtra(originalWay, false, storageExtra.GetId(), storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName, new List<string>())
                        : new StorageExtra(originalWay, false, 0, storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName, new List<string>()))
                .ToList();

        public List<StorageExtra> UnCompressingObjectsToDifferentLocation(StorageExtra storageExtra, string locationWay)
            => storageExtra.ImmutableOriginalWays.Select(originalWay =>
                        storageExtra.CanGetId()
                    ? new StorageExtra(Path.Combine(locationWay, ObjectNameWithoutExtension(originalWay)), false, storageExtra.GetId(), storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName, new List<string>())
                    : new StorageExtra(Path.Combine(locationWay, ObjectNameWithoutExtension(originalWay)), false, 0, storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName, new List<string>()))
                .ToList();

        public void UnCompressingObject(string sourceFile, string targetDirectory)
        {
        }

        public bool CanUncompressing(StorageExtra storageExtra)
            => storageExtra.IsZipping;

        public void DeleteStorageExtraFromRepository(StorageExtra storageExtra)
        {
        }

        public bool CanDeleteStorageExtraFromRepository(StorageExtra storageExtra)
            => true;

        public StorageExtra CopyStorageExtra(StorageExtra storageExtra)
            => storageExtra.CanGetId()
                ? new StorageExtra(storageExtra.Way, storageExtra.IsZipping, storageExtra.GetId(), storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName, storageExtra.ImmutableOriginalWays.ToList())
                : new StorageExtra(storageExtra.Way, storageExtra.IsZipping, 0, storageExtra.StorageAlgorithmExtraType, storageExtra.CompressingName, storageExtra.ImmutableOriginalWays.ToList());

        public void MergeTwoRestorePointExtras(
            RestorePointExtra oldRestorePointExtra,
            RestorePointExtra newRestorePointExtra,
            BackUpExtra backUpExtra,
            bool isSplitAlgorithm)
        {
            if (isSplitAlgorithm)
            {
                foreach (StorageExtra oldStorageExtra in oldRestorePointExtra.StoragesExtra)
                {
                    bool isInNewRestorePoint = true;
                    foreach (StorageExtra newStorageExtra in newRestorePointExtra.StoragesExtra)
                    {
                        if (oldStorageExtra.GetId() == newStorageExtra.GetId())
                        {
                            oldRestorePointExtra.StoragesExtra.Remove(oldStorageExtra);
                            isInNewRestorePoint = false;
                            break;
                        }
                    }

                    if (isInNewRestorePoint)
                    {
                        StorageExtra storageExtra = CopyStorageExtra(oldStorageExtra);
                        oldRestorePointExtra.StoragesExtra.Remove(oldStorageExtra);
                        newRestorePointExtra.StoragesExtra.Add(storageExtra);
                    }
                }
            }

            foreach (RestorePointExtra restorePointExtra in backUpExtra.ImmutableRestorePointExtraList)
            {
                if (restorePointExtra.IsTheSameIdWith(oldRestorePointExtra))
                {
                    backUpExtra.DeleteRestorePoint(restorePointExtra);
                    return;
                }
            }
        }
    }
}