using System;
using System.Collections.Immutable;
using Banks.Services.UI.Commands.CommandPrint.CommandPrintConcrete;
using Banks.Services.UI.Commands.Helpers;
using Banks.Tools.Banks;
using Banks.Tools.CentralBankTools;
using Banks.Tools.ClientPart;

namespace Banks.Services.UI.Commands.CommandPrint
{
    public class PrintClients : ICommand
    {
        private IUserInterface _userInterface;

        public PrintClients(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            bool isNotCorrectCommand = true;
            while (isNotCorrectCommand)
            {
                switch (_userInterface.WriteAndRead("In someone bank or all?"))
                {
                    case "bank":
                        string bankIdString = _userInterface.WriteAndRead("Enter bank id.");
                        while (!IsCorrectIdHelper.IsUint(bankIdString) && IsCorrectIdHelper.IsCorrectBankId(centralBank, bankIdString))
                            bankIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                        uint bankId = uint.Parse(bankIdString);
                        foreach (Client client in centralBank.BankClients(bankId).ImmutableClients)
                        {
                            new PrintConcreteClient(_userInterface, client.Id).RunCommand(out shouldQuit, centralBank);
                        }

                        return true;
                    case "all":
                        foreach (Bank bank in centralBank.AllBanks)
                        {
                            foreach (Client client in centralBank.BankClients(bank.Id).ImmutableClients)
                            {
                                new PrintConcreteClient(_userInterface, client.Id).RunCommand(out shouldQuit, centralBank);
                            }
                        }

                        return true;
                    default:
                        _userInterface.Write("Not correct command. Try again. Enter (bank / all)");
                        isNotCorrectCommand = false;
                        break;
                }
            }

            return true;
        }
    }
}