using Backups.Services;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.ClearerClass;
using BackupsExtra.Tools.ClearingAlgorithm;
using BackupsExtra.Tools.RepositoryExtra;
using BackupsExtra.Tools.StorageAlgorithmExtra;
using Newtonsoft.Json;

namespace BackupsExtra.Services
{
    public class BackUpJobExtra : BackUpJob
    {
        [JsonProperty]
        private ISelectingAlgorithm _selectingAlgorithm;
        [JsonProperty]
        private IClearer _clearer;

        public BackUpJobExtra(IRepositoryExtra repository, IStorageAlgorithmExtra storageAlgorithm, ISelectingAlgorithm selectingAlgorithm, string backUpName, IClearer clearer)
            : base(repository, storageAlgorithm, backUpName)
        {
            _selectingAlgorithm = selectingAlgorithm;
            _clearer = clearer;
            Algorithm = storageAlgorithm;
            BackUp = new BackUpExtra(backUpName);
        }

        [JsonProperty]
        protected new BackUpExtra BackUp { get; }
        [JsonProperty]
        protected new IStorageAlgorithmExtra Algorithm { get; }
        [JsonProperty]
        protected new IRepositoryExtra Repository { get; set; }

        public new RestorePointExtra MakeRestorePoint() => BackUp.MakeRestorePoint(JobObjects, Repository, Algorithm);

        public void ClearSelectingRestorePoints()
        {
            _clearer.ClearRestoresPointExtra(_selectingAlgorithm, BackUp, Algorithm.GetStorageAlgorithmExtraType() == StorageAlgorithmExtraType.SplitType);
        }
    }
}