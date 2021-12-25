using Banks.Exceptions;
using Banks.Tools;

namespace Banks.Services.UI.Commands.CommandBlock
{
    public class BlockClient : ICommand
    {
        private IUserInterface _userInterface;
        private uint _id;
        public BlockClient(IUserInterface userInterface, uint id)
        {
            _userInterface = userInterface;
            _id = id;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            try
            {
                centralBank.BlockClient(_id);
                _userInterface.Write($"Client {centralBank.Clients.FindClient(_id).Name} blocked.");
                return true;
            }
            catch (BankException)
            {
                return false;
            }
        }
    }
}