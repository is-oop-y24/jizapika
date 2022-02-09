using System.Collections.Generic;
using Backups.Tools.BackUpClasses;

namespace BackupsExtra.Tools.ClearingAlgorithm
{
    public interface IClearingAlgorithm
    {
        public List<RestorePoint> GetRestorePointsForClearing(List<RestorePoint> restorePointList);
    }
}