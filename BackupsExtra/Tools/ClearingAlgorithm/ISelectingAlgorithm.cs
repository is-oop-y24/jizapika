using System.Collections.Generic;
using Backups.Tools.BackUpClasses;

namespace BackupsExtra.Tools.ClearingAlgorithm
{
    public interface ISelectingAlgorithm
    {
        public List<RestorePoint> GetRestorePointsForClearing(List<RestorePoint> restorePointList);
    }
}