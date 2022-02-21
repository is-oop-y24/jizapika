using Banks.Services.UI.Commands.CommandPrint.CommandPrintConcrete;
using Banks.Services.UI.Commands.Helpers;
using Banks.Tools.Accounts;
using Banks.Tools.Banks;
using Banks.Tools.CentralBankTools;
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
                switch (_userInterface.WriteAndRead("At someone client, in someone bank or all? "))
                {
                    case "client":
                        string clientIdString = _userInterface.WriteAndRead("Enter client id.");
                        while (!IsCorrectIdHelper.IsUint(clientIdString) && IsCorrectIdHelper.IsCorrectBankId(centralBank, clientIdString))
                            clientIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                        uint clientId = uint.Parse(clientIdString);
                        foreach (Account account in centralBank.ClientAccounts(clientId).ImmutableAccounts)
                        {
                            new PrintConcreteAccount(_userInterface, account.Id).RunCommand(out shouldQuit, centralBank);
                        }

                        return true;
                    case "bank":
                        string bankIdString = _userInterface.WriteAndRead("Enter bank id.");
                        while (!IsCorrectIdHelper.IsUint(bankIdString) && IsCorrectIdHelper.IsCorrectBankId(centralBank, bankIdString))
                            bankIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                        uint bankId = uint.Parse(bankIdString);
                        foreach (Client client in centralBank.BankClients(bankId).ImmutableClients)
                        {
                            foreach (Account account in centralBank.ClientAccounts(client.Id).ImmutableAccounts)
                            {
                                new PrintConcreteAccount(_userInterface, account.Id).RunCommand(out shouldQuit, centralBank);
                            }
                        }

                        return true;
                    case "all":
                        foreach (Bank bank in centralBank.AllBanks)
                        {
                            foreach (Client client in centralBank.BankClients(bank.Id).ImmutableClients)
                            {
                                foreach (Account account in centralBank.ClientAccounts(client.Id).ImmutableAccounts)
                                {
                                    new PrintConcreteAccount(_userInterface, account.Id).RunCommand(out shouldQuit, centralBank);
                                }
                            }
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
    }
}