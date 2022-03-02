using System;

namespace Banks.Services.UI
{
    public interface IUserInterface
    {
        public string WriteAndRead(string message);
        public void Write(string message);
    }
}