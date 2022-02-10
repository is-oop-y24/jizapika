using Backups.Tools.StorageAlgorithm;

namespace BackupsExtra.Tools.StorageAlgorithmExtra
{
    public class SingleStorageExtra : SingleStorage, IStorageAlgorithmExtra
    {
        public StorageAlgorithmExtraType GetStorageAlgorithmExtraType()
            => StorageAlgorithmExtraType.Single;
    }
}