using Banks.Exceptions;
using Banks.Tools;

namespace Banks.Services.UI.Commands.CommandQuit
{
    public class QuitCommand : ICommand
    {
        private IUserInterface _userInterface;

        public QuitCommand(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = true;
            try
            {
                _userInterface.Write("Thanks for using.");
                return true;
            }
            catch (BankException)
            {
                return false;
            }
        }
    }
}