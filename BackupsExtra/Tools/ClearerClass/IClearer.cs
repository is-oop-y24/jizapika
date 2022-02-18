using BackupsExtra.Tools.BackUpExtraClasses;
using BackupsExtra.Tools.ClearingAlgorithm;
using BackupsExtra.Tools.RepositoryExtra;

namespace BackupsExtra.Tools.ClearerClass
{
    public interface IClearer
    {
        public void ClearRestoresPointExtra(
            ISelectingAlgorithm selectingAlgorithm,
            BackUpExtra backUpExtra,
            IRepositoryExtra repositoryExtra,
            bool isSplitAlgorithm);
    }
}