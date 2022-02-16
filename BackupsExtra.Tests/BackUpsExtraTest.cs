using Backups.Tools.JobObjectsClasses;
using BackupsExtra.Services;
using BackupsExtra.Tools.ClearerClass;
using BackupsExtra.Tools.ClearingAlgorithm;
using BackupsExtra.Tools.RepositoryExtra;
using BackupsExtra.Tools.StorageAlgorithmExtra;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    [TestFixture]
    public class BackUpsTest
    {
        [Test]
        public void DeletingJobObjectOnBackUpJob_QuantityOfStoragesAndQuantityOfRestorePointsAreCorrect()
        {
            var abstractRepository = new AbstractFileSystemExtra();
            var singleAlgorithm = new SingleStorageExtra();
            var selectingAlgorithm = new SelectingAlgorithmByNumberOfRestorePoints(1);
            var clearerAlgorithm = new Merger(abstractRepository);
            var backUpJob = new BackUpJobExtra(abstractRepository, singleAlgorithm, selectingAlgorithm, "test-1", clearerAlgorithm);
            backUpJob.AddJobObject("A");
            JobObject bJobObject = backUpJob.AddJobObject("B");
            backUpJob.MakeRestorePoint();
            backUpJob.DeleteJobObject(bJobObject);
            backUpJob.MakeRestorePoint();
            Assert.AreEqual(2, backUpJob.QuantityOfRestorePoints());
            Assert.AreEqual(3, backUpJob.QuantityOfStorages());
        }
        
        [Test]
        public void SelectTwoRestorePointsAndMerging_QuantityOfRestorePointsAndQuantityOfRestorePointsAreCorrect()
        {
            
            var abstractRepository = new AbstractFileSystemExtra();
            var singleAlgorithm = new SingleStorageExtra();
            var selectingAlgorithm = new SelectingAlgorithmByNumberOfRestorePoints(2);
            var clearerAlgorithm = new Merger(abstractRepository);
            var backUpJob = new BackUpJobExtra(abstractRepository, singleAlgorithm, selectingAlgorithm, "test-1", clearerAlgorithm);
            JobObject aJobObject = backUpJob.AddJobObject("A");
            JobObject bJobObject = backUpJob.AddJobObject("B");
            backUpJob.MakeRestorePoint();
            backUpJob.DeleteJobObject(bJobObject);
            backUpJob.MakeRestorePoint();
            backUpJob.DeleteJobObject(aJobObject);
            backUpJob.MakeRestorePoint();
            backUpJob.ClearSelectingRestorePoints();
            Assert.AreEqual(2, backUpJob.QuantityOfRestorePoints());
            Assert.AreEqual(1, backUpJob.QuantityOfStorages());
        }
    }
}