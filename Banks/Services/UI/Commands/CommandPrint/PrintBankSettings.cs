using System;
using Banks.Services.UI.Commands.Helpers;
using Banks.Tools.CentralBankTools;

namespace Banks.Services.UI.Commands.CommandPrint
{
    public class PrintBankSettings : ICommand
    {
        private IUserInterface _userInterface;

        public PrintBankSettings(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            string bankIdString = _userInterface.WriteAndRead("Enter bank id.");
            while (!IsCorrectIdHelper.IsUint(bankIdString) && IsCorrectIdHelper.IsCorrectBankId(centralBank, bankIdString))
                bankIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
            uint bankId = uint.Parse(bankIdString);

            _userInterface.Write($"{centralBank.BankName(bankId)} bank settings:");
            BankSettingsPrinter.SetCreditAccountSettings(
                _userInterface, centralBank.GetCreditAccountSettingsByBankId(bankId));
            BankSettingsPrinter.SetDebitAccountSettings(
                _userInterface, centralBank.GetDebitAccountSettingsByBankId(bankId));
            BankSettingsPrinter.SetDepositAccountSettings(
                _userInterface, centralBank.GetDepositAccountSettingsByBankId(bankId));
            return true;
        }
    }
}