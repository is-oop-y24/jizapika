using Backups.Tools.JobObjectsClasses;
using BackupsExtra.Services;
using BackupsExtra.Tools.ClearerClass;
using BackupsExtra.Tools.ClearingAlgorithm;
using BackupsExtra.Tools.LogFiles;
using BackupsExtra.Tools.RepositoryExtra;
using BackupsExtra.Tools.SerializationClass;
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
            var splitAlgorithm = new SplitStoragesExtra();
            var selectingAlgorithm = new SelectingAlgorithmByNumberOfRestorePoints(1);
            var clearerAlgorithm = new Merger();
            var loggingMethod = new ConsoleLogging(true);
            var backUpJob = new BackUpJobExtra(loggingMethod, abstractRepository, splitAlgorithm, selectingAlgorithm, "test-1", clearerAlgorithm);
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
            var splitAlgorithm = new SplitStoragesExtra();
            var selectingAlgorithm = new SelectingAlgorithmByNumberOfRestorePoints(2);
            var clearerAlgorithm = new Merger();
            var loggingMethod = new ConsoleLogging(true);
            var backUpJob = new BackUpJobExtra(loggingMethod, abstractRepository, splitAlgorithm, selectingAlgorithm,
                "test-1", clearerAlgorithm);
            JobObject aJobObject = backUpJob.AddJobObject("A");
            JobObject bJobObject = backUpJob.AddJobObject("B");
            backUpJob.MakeRestorePoint();
            backUpJob.DeleteJobObject(bJobObject);
            backUpJob.MakeRestorePoint();
            backUpJob.MakeRestorePoint();
            backUpJob.DeleteJobObject(aJobObject);
            backUpJob.MakeRestorePoint();
            backUpJob.ClearSelectingRestorePoints();
            Assert.AreEqual(2, backUpJob.QuantityOfRestorePoints());
            Assert.AreEqual(1, backUpJob.QuantityOfStorages());
        }

        [Test]
        public void MergingAndSerialization_QuantityOfRestorePointsAndQuantityOfRestorePointsAreCorrect()
        {
            var abstractRepository = new AbstractFileSystemExtra();
            var splitAlgorithm = new SplitStoragesExtra();
            var selectingAlgorithm = new SelectingAlgorithmByNumberOfRestorePoints(2);
            var clearerAlgorithm = new Merger();
            var loggingMethod = new ConsoleLogging(true);
            var backUpJob = new BackUpJobExtra(loggingMethod, abstractRepository, splitAlgorithm, selectingAlgorithm,
                "mergingTest", clearerAlgorithm);
            JobObject aJobObject = backUpJob.AddJobObject("A");
            JobObject bJobObject = backUpJob.AddJobObject("B");
            backUpJob.MakeRestorePoint();
            backUpJob.DeleteJobObject(aJobObject);
            backUpJob.MakeRestorePoint();
            backUpJob.MakeRestorePoint();
            backUpJob.DeleteJobObject(bJobObject);
            backUpJob.MakeRestorePoint();
            backUpJob.ClearSelectingRestorePoints();
            string serializingBackUpJob = Serializator.Serialize(backUpJob);
            BackUpJobExtra deserializingBackUpJob = Serializator.Deserialize(serializingBackUpJob);
            Assert.AreEqual(2, deserializingBackUpJob.QuantityOfRestorePoints());
            Assert.AreEqual(1, deserializingBackUpJob.QuantityOfStorages());
        }

        [Test]
        public void SerializationAndMerging_QuantityOfRestorePointsAndQuantityOfRestorePointsAreCorrect()
        {
            var abstractRepository = new AbstractFileSystemExtra();
            var splitAlgorithm = new SplitStoragesExtra();
            var selectingAlgorithm = new SelectingAlgorithmByNumberOfRestorePoints(2);
            var clearerAlgorithm = new Merger();
            var loggingMethod = new ConsoleLogging(true);
            var backUpJob = new BackUpJobExtra(loggingMethod, abstractRepository, splitAlgorithm, selectingAlgorithm, "mergingTest", clearerAlgorithm);
            JobObject aJobObject = backUpJob.AddJobObject("A");
            JobObject bJobObject = backUpJob.AddJobObject("B");
            backUpJob.MakeRestorePoint();
            backUpJob.DeleteJobObject(aJobObject);
            backUpJob.MakeRestorePoint();
            string serializingBackUpJob = Serializator.Serialize(backUpJob);
            BackUpJobExtra deserializingBackUpJob = Serializator.Deserialize(serializingBackUpJob);
            deserializingBackUpJob.MakeRestorePoint();
            deserializingBackUpJob.MakeRestorePoint();
            deserializingBackUpJob.ClearSelectingRestorePoints();
            Assert.AreEqual(2, deserializingBackUpJob.QuantityOfRestorePoints());
            Assert.AreEqual(2, deserializingBackUpJob.QuantityOfStorages());
        }
    }
}