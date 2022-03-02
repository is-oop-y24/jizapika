namespace Backups.Tools.JobObjectsClasses
{
    public class JobObject
    {
        public JobObject(string name)
        {
            Way = name;
        }

        public string Way { get; }
    }
}