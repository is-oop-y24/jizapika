using System;
using Banks.Services.UI.Commands.Helpers;
using Banks.Tools.BankSetting;
using Banks.Tools.BankSetting.BankAccountsSettings;
using Banks.Tools.CentralBankTools;

namespace Banks.Services.UI.Commands.CommandCreate
{
    public class CreateBank : ICommand
    {
        private IUserInterface _userInterface;
        public CreateBank(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            try
            {
                string bankName = _userInterface.WriteAndRead("Enter bank name: ");
                BankSettings settings = EnterSettings();
                centralBank.AddBank_ReturnID(settings, bankName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private BankSettings EnterSettings()
        {
            CreditAccountSettings creditSettings = BankSettingsHelper.CreateCreditAccountSettings(_userInterface);
            DebitAccountSettings debitSettings = BankSettingsHelper.CreateDebitAccountSettings(_userInterface);
            DepositAccountSettings depositSettings = BankSettingsHelper.CreateDepositAccountSettings(_userInterface);
            return new BankSettings(creditSettings, debitSettings, depositSettings);
        }
    }
}