using System;

namespace Banks.Services.UI
{
    public class ConsoleUserInterface : IUserInterface
    {
        public string WriteAndRead(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}