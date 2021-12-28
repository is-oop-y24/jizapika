using Backups.Services;
using Backups.Tools.JobObjectsClasses;
using Backups.Tools.Repository;
using Backups.Tools.StorageAlgorithm;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var repository = new FileSystem("C:\\Users\\Татьяна\\Desktop\\Test3\\root");
            var singleAlgorithm = new SingleStorage();
            var backUpJob = new BackUpJob(repository, singleAlgorithm, "BackUp");
            JobObject lev = backUpJob.AddJobObject("C:\\Users\\Татьяна\\Desktop\\Test3\\JobObjects\\Lev.txt");
            JobObject timur = backUpJob.AddJobObject("C:\\Users\\Татьяна\\Desktop\\Test3\\JobObjects\\Timur.txt");
            backUpJob.MakeRestorePoint();
            backUpJob.MakeRestorePoint();
        }
    }
}
