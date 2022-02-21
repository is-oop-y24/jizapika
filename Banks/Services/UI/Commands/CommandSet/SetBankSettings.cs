using System;
using Banks.Services.UI.Commands.Helpers;
using Banks.Tools.BankSetting.BankAccountsSettings;
using Banks.Tools.CentralBankTools;

namespace Banks.Services.UI.Commands.CommandSet
{
    public class SetBankSettings : ICommand
    {
        private IUserInterface _userInterface;

        public SetBankSettings(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            string bankIdString = _userInterface.WriteAndRead("Enter bank id.");
            while (!IsUint(bankIdString) && IsCorrectBankId(centralBank, bankIdString))
                bankIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
            uint bankId = uint.Parse(bankIdString);

            bool isCorrectCommand = false;
            while (!isCorrectCommand)
            {
                string typeSettings =
                    _userInterface.WriteAndRead(
                        "What's type of settings do you want to set? ( credit / debit / deposit )");
                isCorrectCommand = true;
                switch (typeSettings)
                {
                    case "credit":
                        CreditAccountSettings creditSettings =
                            BankSettingsHelper.CreateCreditAccountSettings(_userInterface);
                        break;
                    case "debit":
                        DebitAccountSettings debitSettings =
                            BankSettingsHelper.CreateDebitAccountSettings(_userInterface);
                        break;
                    case "deposit":
                        DepositAccountSettings depositSettings =
                            BankSettingsHelper.CreateDepositAccountSettings(_userInterface);
                        break;
                    default:
                        _userInterface.Write("Not correct command. Try again.");
                        isCorrectCommand = false;
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
                centralBank.Banks.FindBank(uint.Parse(bankIdString));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}