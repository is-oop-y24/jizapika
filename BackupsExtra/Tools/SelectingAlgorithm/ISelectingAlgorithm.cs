using System.Collections.Generic;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.SelectingAlgorithm
{
    public interface ISelectingAlgorithm
    {
        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList);
        public RestorePointExtra GetFirstNotClearingRestorePoint(List<RestorePointExtra> restorePointExtraList);
    }
}