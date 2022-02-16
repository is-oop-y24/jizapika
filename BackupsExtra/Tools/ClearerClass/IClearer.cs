using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.ClearingAlgorithm;

namespace BackupsExtra.Tools.ClearerClass
{
    public interface IClearer
    {
        public void ClearRestoresPointExtra(
            ISelectingAlgorithm selectingAlgorithm,
            BackUpExtra backUpExtra,
            bool isSplitAlgorithm);
    }
}