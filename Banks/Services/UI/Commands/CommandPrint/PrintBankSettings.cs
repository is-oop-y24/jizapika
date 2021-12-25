using System;
using Banks.Services.UI.Commands.Helpers;
using Banks.Tools;
using Microsoft.VisualBasic;

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
            while (!IsUint(bankIdString) && IsCorrectBankId(centralBank, bankIdString))
                bankIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
            uint bankId = uint.Parse(bankIdString);

            _userInterface.Write($"{centralBank.Banks.FindBank(bankId).Name} bank settings:");
            BankSettingsPrinter.SetCreditAccountSettings(
                _userInterface, centralBank.Banks.FindBank(bankId).Settings.CreditSettings);
            BankSettingsPrinter.SetDebitAccountSettings(
                _userInterface, centralBank.Banks.FindBank(bankId).Settings.DebitSettings);
            BankSettingsPrinter.SetDepositAccountSettings(
                _userInterface, centralBank.Banks.FindBank(bankId).Settings.DepositSettings);
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