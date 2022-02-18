using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.StorageAlgorithmExtra;

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

        public StorageExtra CompressingObjects(List<StorageExtra> storages, string backUpName, string restorePointName, string compressedName)
            => storages[0].CanGetId()
                ? new StorageExtra(string.Empty, true, storages[0].GetId(), storages[0].StorageAlgorithmExtraType, compressedName, storages.Select(storageExtra => storageExtra.Way).ToList())
                : new StorageExtra(string.Empty, true, 0, storages[0].StorageAlgorithmExtraType, compressedName, storages.Select(storageExtra => storageExtra.Way).ToList());

        public StorageExtra CopyObject(JobObject jobObject, uint id, StorageAlgorithmExtraType storageAlgorithmExtraType)
            => new StorageExtra(jobObject.Way, false, id, storageAlgorithmExtraType, jobObject.Way, new List<string>());

        public void MergeTwoRestorePointExtras(
            RestorePointExtra oldRestorePointExtra,
            RestorePointExtra newRestorePointExtra,
            BackUpExtra backUpExtra,
            bool isSplitAlgorithm)
        {
            if (isSplitAlgorithm)
            {
                foreach (StorageExtra oldStorageExtra in oldRestorePointExtra.ImmutableStorages)
                {
                    bool isInNewRestorePoint = true;
                    foreach (StorageExtra newStorageExtra in newRestorePointExtra.ImmutableStorages)
                    {
                        if (oldStorageExtra.GetId() == newStorageExtra.GetId())
                        {
                            isInNewRestorePoint = false;
                            break;
                        }
                    }

                    if (isInNewRestorePoint)
                    {
                        StorageExtra storageExtra = CopyStorageExtra(oldStorageExtra);
                        newRestorePointExtra.AddStorage(storageExtra);
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