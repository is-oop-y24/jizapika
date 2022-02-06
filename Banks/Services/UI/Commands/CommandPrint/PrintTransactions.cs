using System;
using System.Collections.Immutable;
using Banks.Services.UI.Commands.CommandPrint.CommandPrintConcrete;
using Banks.Tools;
using Banks.Tools.Accounts;
using Banks.Tools.ClientPart;
using Transaction = Banks.Tools.Transactions.Transaction;

namespace Banks.Services.UI.Commands.CommandPrint
{
    public class PrintTransactions : ICommand
    {
        private IUserInterface _userInterface;

        public PrintTransactions(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            bool flag = true;
            while (flag)
            {
                switch (_userInterface.WriteAndRead("At someone client, in someone account, bank or all?"))
                {
                    case "account":
                        string accountIdString = _userInterface.WriteAndRead("Enter account id.");
                        while (!IsUint(accountIdString) && IsCorrectBankId(centralBank, accountIdString))
                            accountIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                        uint accountId = uint.Parse(accountIdString);
                        foreach (Transaction transaction in centralBank.Accounts.FindAccount(accountId).AccountTransactions(centralBank.Transactions).ImmutableTransactions)
                        {
                            new PrintConcreteTransaction(_userInterface, transaction.Id).RunCommand(out shouldQuit, centralBank);
                        }

                        return true;
                    case "client":
                        string clientIdString = _userInterface.WriteAndRead("Enter client id.");
                        while (!IsUint(clientIdString) && IsCorrectBankId(centralBank, clientIdString))
                            clientIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                        uint clientId = uint.Parse(clientIdString);
                        foreach (Account account in centralBank.Clients.FindClient(clientId).ClientAccounts(centralBank.Accounts)
                            .ImmutableAccounts)
                        {
                            foreach (Transaction transaction in centralBank.Accounts.FindAccount(account.Id).AccountTransactions(centralBank.Transactions).ImmutableTransactions)
                            {
                                new PrintConcreteTransaction(_userInterface, transaction.Id).RunCommand(out shouldQuit, centralBank);
                            }
                        }

                        return true;
                    case "bank":
                        string bankIdString = _userInterface.WriteAndRead("Enter bank id.");
                        while (!IsUint(bankIdString) && IsCorrectBankId(centralBank, bankIdString))
                            bankIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                        uint bankId = uint.Parse(bankIdString);
                        foreach (Client client in centralBank.Banks.FindBank(bankId).BankClients(centralBank.Clients)
                            .ImmutableClients)
                        {
                            foreach (Account account in centralBank.Clients.FindClient(client.Id).ClientAccounts(centralBank.Accounts).ImmutableAccounts)
                            {
                                new PrintConcreteTransaction(_userInterface, account.Id).RunCommand(out shouldQuit, centralBank);
                            }
                        }

                        return true;
                    case "all":
                        ImmutableList<Transaction> immutableTransactions = centralBank.Transactions.ImmutableTransactions;
                        foreach (Transaction transaction in immutableTransactions)
                        {
                            new PrintConcreteTransaction(_userInterface, transaction.Id).RunCommand(out shouldQuit, centralBank);
                        }

                        return true;
                    default:
                        _userInterface.Write("Not correct command. Try again. Enter (account / client / bank / all)");
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