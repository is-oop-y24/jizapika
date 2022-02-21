using System;
using System.Collections.Immutable;
using Banks.Services.UI.Commands.CommandPrint.CommandPrintConcrete;
using Banks.Tools.Banks;
using Banks.Tools.CentralBankTools;

namespace Banks.Services.UI.Commands.CommandPrint
{
    public class PrintBanks : ICommand
    {
        private IUserInterface _userInterface;

        public PrintBanks(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            try
            {
                ImmutableList<Bank> immutableBanks = centralBank.Banks.ImmutableBanks;
                foreach (Bank bank in immutableBanks)
                {
                    new PrintConcreteBank(_userInterface, bank.Id).RunCommand(out shouldQuit, centralBank);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}