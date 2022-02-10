using Backups.Tools.StorageAlgorithm;

namespace BackupsExtra.Tools.StorageAlgorithmExtra
{
    public interface IStorageAlgorithmExtra : IStorageAlgorithm
    {
        public StorageAlgorithmExtraType GetStorageAlgorithmExtraType();
    }
}