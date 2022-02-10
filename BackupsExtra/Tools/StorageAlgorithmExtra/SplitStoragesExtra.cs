using Backups.Tools.StorageAlgorithm;

namespace BackupsExtra.Tools.StorageAlgorithmExtra
{
    public class SplitStoragesExtra : SplitStorages, IStorageAlgorithmExtra
    {
        public StorageAlgorithmExtraType GetStorageAlgorithmExtraType()
            => StorageAlgorithmExtraType.Split;
    }
}