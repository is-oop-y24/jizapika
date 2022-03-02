using System;
using Banks.Exceptions;
using Banks.Tools.CentralBankTools;

namespace Banks.Services.UI.Commands.CommandCreate
{
    public class CreateAccount : ICommand
    {
        private IUserInterface _userInterface;

        public CreateAccount(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            try
            {
                string clientIdString = _userInterface.WriteAndRead("Enter client id.");
                while (!IsUint(clientIdString) && IsCorrectClientId(centralBank, clientIdString))
                    clientIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                uint clientId = uint.Parse(clientIdString);

                string accountType =
                    _userInterface.WriteAndRead("Enter type of new account ( credit, debit or deposit ).");

                bool flagOpened = true;
                while (flagOpened)
                {
                    switch (accountType)
                    {
                        case "credit":
                            centralBank.AddCreditAccount_ReturnID(clientId);
                            flagOpened = false;
                            break;
                        case "debit":
                            centralBank.AddDebitAccount_ReturnID(clientId);
                            flagOpened = false;
                            break;
                        case "deposit":
                            centralBank.AddDepositAccount_ReturnID(clientId);
                            flagOpened = false;
                            break;
                        default:
                            accountType =
                                _userInterface.WriteAndRead("Enter type of new account ( credit, debit or deposit ).");
                            break;
                    }
                }

                return true;
            }
            catch (BankException)
            {
                return false;
            }
        }

        private bool IsUint(string command)
        {
            if (command.Split(' ').Length > 1) return false;
            if (!uint.TryParse(command.Split(' ')[0], out _)) return false;
            return true;
        }

        private bool IsCorrectClientId(CentralBank centralBank, string clientIdString)
        {
            try
            {
                return centralBank.IsCorrectClientId(uint.Parse(clientIdString));
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}