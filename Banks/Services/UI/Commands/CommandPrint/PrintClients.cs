using System;
using System.Collections.Immutable;
using Banks.Services.UI.Commands.CommandPrint.CommandPrintConcrete;
using Banks.Tools;
using Banks.Tools.Banks;
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
            bool flag = true;
            while (flag)
            {
                switch (_userInterface.WriteAndRead("In someone bank or all?"))
                {
                    case "bank":
                        string bankIdString = _userInterface.WriteAndRead("Enter bank id.");
                        while (!IsUint(bankIdString) && IsCorrectBankId(centralBank, bankIdString))
                            bankIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                        uint bankId = uint.Parse(bankIdString);
                        foreach (Client client in centralBank.Banks.FindBank(bankId).BankClients(centralBank.Clients)
                            .Clients)
                        {
                            new PrintConcreteClient(_userInterface, client.Id).RunCommand(out shouldQuit, centralBank);
                        }

                        return true;
                    case "all":
                        ImmutableList<Client> immutableClients = centralBank.Clients.ImmutableClients;
                        foreach (Client client in immutableClients)
                        {
                            new PrintConcreteClient(_userInterface, client.Id).RunCommand(out shouldQuit, centralBank);
                        }

                        return true;
                    default:
                        _userInterface.Write("Not correct command. Try again. Enter (bank / all)");
                        flag = false;
                        break;
                }
            }

            return true;
        }

        private bool IsUint(string command)
        {
            if (command.Split(' ').Length > 1) return false;
            if (!uint.TryParse(command.Split(' ')[0], out _)) return false;
            return true;
        }

        private bool IsCorrectBankId(CentralBank centralBank, string bankIdString)
        {
            try
            {
                centralBank.Clients.FindClient(uint.Parse(bankIdString));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}