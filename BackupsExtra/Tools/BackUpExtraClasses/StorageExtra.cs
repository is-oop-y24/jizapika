using Backups.Exceptions;
using Backups.Tools.BackUpClasses;
using Backups.Tools.StorageAlgorithm;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.StorageAlgorithmExtra;

namespace BackupsExtra.Tools.BackUpExtraClasses
{
    public class StorageExtra : Storage
    {
        private StorageAlgorithmExtraType _storageAlgorithmExtraType;
        private uint _id;
        public StorageExtra(string way, bool isZipping, uint id, StorageAlgorithmExtraType storageAlgorithmExtraType) : base(way, isZipping)
        {
            _id = id;
            _storageAlgorithmExtraType = storageAlgorithmExtraType;
        }

        public uint GetId()
        {
            if (IsZipping && _storageAlgorithmExtraType == StorageAlgorithmExtraType.Split) return _id;
            throw new BackUpsExtraExceptions($"The storage have null id.");
        }
    }
}