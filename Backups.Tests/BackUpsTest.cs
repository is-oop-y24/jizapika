using Backups.Services;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using Backups.Tools.StorageAlgorithm;
using NUnit.Framework;

namespace Backups.Tests
{
    [TestFixture]
    public class BackUpsTest
    {
        [Test]
        public void Test1()
        {
            var abstractRepository = new AbstractFileSystem();
            var splitAlgorithm = new SplitStorages();
            var backUpJob = new BackUpJob(abstractRepository, splitAlgorithm, "Test1");
            backUpJob.AddJobObject("A");
            JobObject bJobObject = backUpJob.AddJobObject("B");
            backUpJob.MakeRestorePoint();
            backUpJob.DeleteJobObject(bJobObject);
            backUpJob.MakeRestorePoint();
            Assert.AreEqual(2, backUpJob.QuantityOfRestorePoints());
            Assert.AreEqual(3, backUpJob.QuantityOfStorages());
        }
        
        [Test]
        public void Test2()
        {
            var abstractRepository = new AbstractFileSystem();
            var singleAlgorithm = new SingleStorage();
            var backUpJob = new BackUpJob(abstractRepository, singleAlgorithm, "Test1");
            backUpJob.AddJobObject("A");
            backUpJob.AddJobObject("B");
            backUpJob.MakeRestorePoint();
            Assert.AreEqual(1, backUpJob.QuantityOfStorages());
        }
    }
}