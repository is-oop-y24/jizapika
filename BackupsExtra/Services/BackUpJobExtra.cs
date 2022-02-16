using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Backups.Services;
using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.ClearerClass;
using BackupsExtra.Tools.ClearingAlgorithm;
using BackupsExtra.Tools.RepositoryExtra;
using BackupsExtra.Tools.StorageAlgorithmExtra;

namespace BackupsExtra.Services
{
    [Serializable]
    public class BackUpJobExtra : BackUpJob
    {
        private ISelectingAlgorithm _selectingAlgorithm;
        private IClearer _clearer;

        public BackUpJobExtra(IRepositoryExtra repository, IStorageAlgorithmExtra storageAlgorithm, ISelectingAlgorithm selectingAlgorithm, string backUpName, IClearer clearer)
            : base(repository, storageAlgorithm, backUpName)
        {
            _selectingAlgorithm = selectingAlgorithm;
            _clearer = clearer;
            Algorithm = storageAlgorithm;
            BackUpExtra = new BackUpExtra(backUpName);
        }

        protected BackUpExtra BackUpExtra { get; }
        protected new IStorageAlgorithmExtra Algorithm { get; }

        public new int QuantityOfRestorePoints() => BackUp.ImmutableRestorePointList.Count;

        public new int QuantityOfStorages() => BackUp.ImmutableRestorePointList.Sum(restorePoint => restorePoint.ImmutableStorages.Count);

        public void ClearSelectingRestorePoints()
        {
            _clearer.ClearRestoresPointExtra(_selectingAlgorithm, BackUpExtra, Algorithm.GetStorageAlgorithmExtraType() == StorageAlgorithmExtraType.SplitType);
        }

        public void SerializationBackUpJobExtra(string path)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this);
            }
        }

        public BackUpJobExtra DeserializationBackUpJobExtra(string path)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (BackUpJobExtra)formatter.Deserialize(fs);
            }
        }
    }
}