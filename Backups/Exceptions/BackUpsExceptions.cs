using System;

namespace Backups.Exceptions
{
    public class BackUpsExceptions : Exception
    {
        public BackUpsExceptions()
        {
        }

        public BackUpsExceptions(string message)
            : base(message)
        {
        }

        public BackUpsExceptions(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}