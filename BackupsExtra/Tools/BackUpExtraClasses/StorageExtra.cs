using System.Collections.Generic;
using System.Collections.Immutable;
using Backups.Exceptions;
using Backups.Tools.BackUpClasses;
using Backups.Tools.StorageAlgorithm;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.StorageAlgorithmExtra;

namespace BackupsExtra.Tools.BackUpExtraClasses
{
    public class StorageExtra : Storage
    {
        private uint _id;
        private List<string> _originalWays;

        public StorageExtra(string way, bool isZipping, uint id, StorageAlgorithmExtraType storageAlgorithmExtraType, string compressingName, List<string> originalWays)
            : base(way, isZipping)
        {
            _id = id;
            StorageAlgorithmExtraType = storageAlgorithmExtraType;
            CompressingName = compressingName;
            _originalWays = originalWays;
        }

        public StorageAlgorithmExtraType StorageAlgorithmExtraType { get; }
        public string CompressingName { get; }
        public ImmutableList<string> ImmutableOriginalWays => _originalWays.ToImmutableList();

        public uint GetId()
        {
            if (IsZipping && StorageAlgorithmExtraType == StorageAlgorithmExtraType.SplitType) return _id;
            throw new BackUpsExtraExceptions($"The storage have null id.");
        }

        public bool CanGetId()
            => IsZipping && StorageAlgorithmExtraType == StorageAlgorithmExtraType.SplitType;
    }
}