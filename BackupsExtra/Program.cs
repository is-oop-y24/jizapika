using System;
using Backups.Tools.JobObjectsClasses;
using BackupsExtra.Services;
using BackupsExtra.Tools.ClearerClass;
using BackupsExtra.Tools.ClearingAlgorithm;
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
            var singleAlgorithm = new SingleStorageExtra();
            var selectingAlgorithm = new SelectingAlgorithmByNumberOfRestorePoints(2);
            var clearerAlgorithm = new Merger(abstractRepository);
            var backUpJob = new BackUpJobExtra(abstractRepository, singleAlgorithm, selectingAlgorithm, "test-1", clearerAlgorithm);
            JobObject lev = backUpJob.AddJobObject("C:\\Users\\Татьяна\\Desktop\\Test3\\JobObjects\\Lev.txt");
            JobObject timur = backUpJob.AddJobObject("C:\\Users\\Татьяна\\Desktop\\Test3\\JobObjects\\Timur.txt");
            backUpJob.MakeRestorePoint();
            backUpJob.MakeRestorePoint();
            string serializingBackUpJob = Serializator.Serialize(backUpJob);
            Console.WriteLine(serializingBackUpJob);
            /*BackUpJobExtra deserializingBackUpJob = Serializator.Deserialize(serializingBackUpJob);
            if (deserializingBackUpJob is not null)
            {
                Console.WriteLine(deserializingBackUpJob.QuantityOfRestorePoints());
                Console.WriteLine(deserializingBackUpJob.QuantityOfStorages());
            }*/
        }
    }
}
