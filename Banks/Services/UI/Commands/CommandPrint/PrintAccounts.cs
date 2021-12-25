using System;
using System.Collections.Immutable;
using Banks.Services.UI.Commands.CommandPrint.CommandPrintConcrete;
using Banks.Tools;
using Banks.Tools.Accounts;
using Banks.Tools.ClientPart;

namespace Banks.Services.UI.Commands.CommandPrint
{
    public class PrintAccounts : ICommand
    {
        private IUserInterface _userInterface;

        public PrintAccounts(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            bool flag = true;
            while (flag)
            {
                switch (_userInterface.WriteAndRead("At someone client, in someone bank or all?"))
                {
                    case "client":
                        string clientIdString = _userInterface.WriteAndRead("Enter client id.");
                        while (!IsUint(clientIdString) && IsCorrectBankId(centralBank, clientIdString))
                            clientIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                        uint clientId = uint.Parse(clientIdString);
                        foreach (Account account in centralBank.Clients.FindClient(clientId).ClientAccounts(centralBank.Accounts).Accounts)
                        {
                            new PrintConcreteAccount(_userInterface, account.Id).RunCommand(out shouldQuit, centralBank);
                        }

                        return true;
                    case "bank":
                        string bankIdString = _userInterface.WriteAndRead("Enter bank id.");
                        while (!IsUint(bankIdString) && IsCorrectBankId(centralBank, bankIdString))
                            bankIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                        uint bankId = uint.Parse(bankIdString);
                        foreach (Client client in centralBank.Banks.FindBank(bankId).BankClients(centralBank.Clients)
                            .Clients)
                        {
                            foreach (Account account in centralBank.Clients.FindClient(client.Id).ClientAccounts(centralBank.Accounts).Accounts)
                            {
                                new PrintConcreteAccount(_userInterface, account.Id).RunCommand(out shouldQuit, centralBank);
                            }
                        }

                        return true;
                    case "all":
                        ImmutableList<Account> immutableAccounts = centralBank.Accounts.ImmutableAccounts;
                        foreach (Account account in immutableAccounts)
                        {
                            new PrintConcreteClient(_userInterface, account.Id).RunCommand(out shouldQuit, centralBank);
                        }

                        return true;
                    default:
                        _userInterface.Write("Not correct command. Try again. Enter (client / bank / all)");
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