namespace Backups.Tools.BackUpClasses
{
    public class Storage
    {
        public Storage(string way, bool isZipping)
        {
            Way = way;
            IsZipping = isZipping;
        }

        public bool IsZipping { get; }
        public string Way { get; }
    }
}