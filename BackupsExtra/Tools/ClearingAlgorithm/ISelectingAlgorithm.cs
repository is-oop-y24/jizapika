using System.Collections.Generic;
using BackupsExtra.Tools.BackUpExtraClasses;

namespace BackupsExtra.Tools.ClearingAlgorithm
{
    public interface ISelectingAlgorithm
    {
        public LinkedList<RestorePointExtra> GetRestorePointExtrasForClearing(LinkedList<RestorePointExtra> restorePointExtraList);
    }
}