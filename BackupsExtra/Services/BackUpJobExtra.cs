using System.Linq;
using Backups.Services;
using Backups.Tools.JobObjectsClasses;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.ClearerClass;
using BackupsExtra.Tools.ClearingAlgorithm;
using BackupsExtra.Tools.LogFiles;
using BackupsExtra.Tools.RepositoryExtra;
using BackupsExtra.Tools.StorageAlgorithmExtra;
using Newtonsoft.Json;

namespace BackupsExtra.Services
{
    public class BackUpJobExtra : BackUpJob
    {
        public BackUpJobExtra(ILogging loggingMethod, IRepositoryExtra repository, IStorageAlgorithmExtra storageAlgorithm, ISelectingAlgorithm selectingAlgorithm, string backUpName, IClearer clearer)
            : base(repository, storageAlgorithm, backUpName)
        {
            Logging = loggingMethod;
            SelectingAlgorithm = selectingAlgorithm;
            Clearer = clearer;
            AlgorithmExtra = storageAlgorithm;
            RepositoryExtra = repository;
            BackupExtra = new BackUpExtra(backUpName);
            BackUpName = backUpName;
            JobObjects = new JobObjects();
        }

        [JsonConstructor]
        private BackUpJobExtra(ILogging logging, IRepositoryExtra repositoryExtra, IStorageAlgorithmExtra algorithmExtra, ISelectingAlgorithm selectingAlgorithm, string backUpName, IClearer clearer, BackUpExtra backupExtra = null, JobObjects jobObjects = null)
            : base(repositoryExtra, algorithmExtra, backUpName)
        {
            Logging = logging;
            SelectingAlgorithm = selectingAlgorithm;
            Clearer = clearer;
            AlgorithmExtra = algorithmExtra;
            RepositoryExtra = repositoryExtra;
            BackupExtra = backupExtra ?? new BackUpExtra(backUpName);
            BackUpName = backUpName;
            JobObjects = jobObjects ?? new JobObjects();
        }

        [JsonProperty]
        private string BackUpName { get; }
        [JsonProperty]
        private BackUpExtra BackupExtra { get; }
        [JsonProperty]
        private IStorageAlgorithmExtra AlgorithmExtra { get; }
        [JsonProperty]
        private IRepositoryExtra RepositoryExtra { get; set; }
        [JsonProperty]
        private ISelectingAlgorithm SelectingAlgorithm { get; set; }
        [JsonProperty]
        private IClearer Clearer { get; set; }
        [JsonProperty]
        private ILogging Logging { get; set; }

        public new RestorePointExtra MakeRestorePoint()
        {
            Logging.Info("Begin make new restore point.");
            RestorePointExtra restorePointExtra = BackupExtra.MakeRestorePoint(JobObjects, RepositoryExtra, AlgorithmExtra);
            Logging.Info($"Making restore point was completed successfully. Time of creation: {restorePointExtra.Time}.");
            return restorePointExtra;
        }

        public new int QuantityOfRestorePoints() => BackupExtra.ImmutableRestorePointExtraList.Count;

        public new int QuantityOfStorages() => BackupExtra.ImmutableRestorePointExtraList.Sum(restorePoint => restorePoint.ImmutableStorages.Count);

        public void ClearSelectingRestorePoints()
        {
            Logging.Info("Begin clearing restore points.");
            Clearer.ClearRestoresPointExtra(SelectingAlgorithm, BackupExtra, RepositoryExtra, AlgorithmExtra.GetStorageAlgorithmExtraType() == StorageAlgorithmExtraType.SplitType);
            Logging.Info("Clearing was completed successfully.");
        }
    }
}