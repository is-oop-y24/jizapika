using System;
using Backups.Tools.JobObjectsClasses;
using BackupsExtra.Services;
using BackupsExtra.Tools.ClearerClass;
using BackupsExtra.Tools.ClearingAlgorithm;
using BackupsExtra.Tools.LogFiles;
using BackupsExtra.Tools.RepositoryExtra;
using BackupsExtra.Tools.SerializationClass;
using BackupsExtra.Tools.StorageAlgorithmExtra;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            var abstractRepository = new AbstractFileSystemExtra();
            var splitAlgorithm = new SplitStoragesExtra();
            var selectingAlgorithm = new SelectingAlgorithmByNumberOfRestorePoints(2);
            var clearerAlgorithm = new Merger();
            var loggingMethod = new ConsoleLogging(true);
            var backUpJob = new BackUpJobExtra(loggingMethod, abstractRepository, splitAlgorithm, selectingAlgorithm, "test-1", clearerAlgorithm);
            JobObject lev = backUpJob.AddJobObject("C:\\Users\\Татьяна\\Desktop\\Test3\\JobObjects\\Lev.txt");
            JobObject timur = backUpJob.AddJobObject("C:\\Users\\Татьяна\\Desktop\\Test3\\JobObjects\\Timur.txt");
            backUpJob.MakeRestorePoint();
            backUpJob.DeleteJobObject(lev);
            backUpJob.MakeRestorePoint();
            Console.WriteLine(backUpJob.QuantityOfRestorePoints());
            Console.WriteLine(backUpJob.QuantityOfStorages());
            string serializingBackUpJob = Serializator.Serialize(backUpJob);
            Console.WriteLine(serializingBackUpJob);
            BackUpJobExtra deserializingBackUpJob = Serializator.Deserialize(serializingBackUpJob);
            if (deserializingBackUpJob is not null)
            {
                Console.WriteLine(deserializingBackUpJob.QuantityOfRestorePoints());
                Console.WriteLine(deserializingBackUpJob.QuantityOfStorages());
            }
        }
    }
}
