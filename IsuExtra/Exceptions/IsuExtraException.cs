using System;
using IsuExtra.OldIsu.Exceptions;

namespace IsuExtra.Exceptions
{
    public class IsuExtraException : IsuException
    {
        public IsuExtraException()
        {
        }

        public IsuExtraException(string message)
            : base(message)
        {
        }

        public IsuExtraException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}