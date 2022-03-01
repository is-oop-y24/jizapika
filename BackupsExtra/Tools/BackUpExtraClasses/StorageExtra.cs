using System.Collections.Generic;
using System.Collections.Immutable;
using Backups.Tools.BackUpClasses;
using BackupsExtra.Exceptions;
using BackupsExtra.Tools.StorageAlgorithmExtra;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.BackUpExtraClasses
{
    public class StorageExtra : Storage
    {
        public StorageExtra(string way, bool isZipping, uint id, StorageAlgorithmExtraType storageAlgorithmExtraType, string compressingName, List<string> originalWays)
            : base(way, isZipping)
        {
            Id = id;
            StorageAlgorithmExtraType = storageAlgorithmExtraType;
            CompressingName = compressingName;
            OriginalWays = originalWays;
        }

        public StorageAlgorithmExtraType StorageAlgorithmExtraType { get; }
        public string CompressingName { get; }
        [JsonIgnore]
        public ImmutableList<string> ImmutableOriginalWays => OriginalWays.ToImmutableList();
        [JsonProperty]
        private uint Id { get; }
        [JsonProperty]
        private List<string> OriginalWays { get; }

        public uint GetId()
        {
            if (IsZipping && StorageAlgorithmExtraType == StorageAlgorithmExtraType.SplitType) return Id;
            throw new BackUpsExtraExceptions($"The storage have null id.");
        }

        public bool CanGetId()
            => IsZipping && StorageAlgorithmExtraType == StorageAlgorithmExtraType.SplitType;
    }
}