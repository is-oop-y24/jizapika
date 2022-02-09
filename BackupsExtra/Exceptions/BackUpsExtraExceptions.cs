using System;
using Backups.Exceptions;

namespace BackupsExtra.Exceptions
{
    public class BackUpsExtraExceptions : BackUpsExceptions
    {
        public BackUpsExtraExceptions()
        {
        }

        public BackUpsExtraExceptions(string message)
            : base(message)
        {
        }

        public BackUpsExtraExceptions(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}