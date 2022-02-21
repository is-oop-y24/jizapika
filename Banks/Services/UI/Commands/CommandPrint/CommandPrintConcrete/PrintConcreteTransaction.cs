using System;
using Banks.Tools.CentralBankTools;
using Banks.Tools.Transactions;

namespace Banks.Services.UI.Commands.CommandPrint.CommandPrintConcrete
{
    public class PrintConcreteTransaction : ICommand
    {
        private uint _id;
        private IUserInterface _userInterface;

        public PrintConcreteTransaction(IUserInterface userInterface, uint id)
        {
            _id = id;
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            try
            {
                TransactionType transactionType = centralBank.TransactionType(_id);
                _userInterface.Write($"id: {_id}, "
                                     + $"transaction type: {transactionType}, "
                                     + $"ammount: {centralBank.TransactionAmmount(_id)}.");
                return true;
            }
            catch (Exception)
            {
                _userInterface.Write("Maybe id not correct. Try to command 'print banks' for watch correct ids.");
                return false;
            }
        }
    }
}