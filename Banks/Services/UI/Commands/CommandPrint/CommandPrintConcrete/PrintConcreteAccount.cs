using System;
using Banks.Tools;
using Banks.Tools.Accounts;

namespace Banks.Services.UI.Commands.CommandPrint.CommandPrintConcrete
{
    public class PrintConcreteAccount : ICommand
    {
        private uint _id;
        private IUserInterface _userInterface;

        public PrintConcreteAccount(IUserInterface userInterface, uint id)
        {
            _id = id;
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            try
            {
                string accountType = centralBank.Accounts.FindAccount(_id).Type();
                _userInterface.Write($"id: {_id}, "
                                     + $"account type: {accountType}, "
                                     + $"sum: {centralBank.Accounts.FindAccount(_id).Sum}. ");
                return true;
            }
            catch (Exception)
            {
                _userInterface.Write("Maybe id not correct. Try to command 'print accounts' for watch correct ids.");
                return false;
            }
        }
    }
}