using System.Collections.Generic;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.ClearingAlgorithm
{
    public interface ISelectingAlgorithm
    {
        public List<RestorePointExtra> GetRestorePointExtrasForClearing(List<RestorePointExtra> restorePointExtraList);
    }
}