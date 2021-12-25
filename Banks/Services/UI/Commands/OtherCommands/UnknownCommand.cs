using Banks.Services.UI.Commands;
using Banks.Tools;

namespace Banks.Services.UI.Commands.OtherCommands
{
    public class UnknownCommand : ICommand
    {
        private IUserInterface _userInterface;
        public UnknownCommand(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            _userInterface.Write("Not correct enter. Let's try again. If you want to read instruction, enter !");
            return true;
        }
    }
}