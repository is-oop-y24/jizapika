using Banks.Services.UI;
using Banks.Services.UI.Commands;
using Banks.Tools.CentralBankTools;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var centralBank = new CentralBank();

            bool shouldQuit = false;
            IUserInterface userInterface = new ConsoleUserInterface();
            userInterface.Write("You are greeted by the banking system.");
            userInterface.Write("If you want to read instruction, enter: ?");
            var commandDirector = new CommandDirector(userInterface);
            while (!shouldQuit)
            {
                string input = userInterface.WriteAndRead("Please, enter your command: ");
                ICommand command = commandDirector.Command(input);

                bool wasSuccessful = command.RunCommand(out shouldQuit, centralBank);
                if (!wasSuccessful)
                {
                    userInterface.Write("Command can't run. Enter '?' to read instruction.");
                }
            }
        }
    }
}
