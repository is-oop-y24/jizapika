using System;
using System.Collections.Generic;
using Banks.Tools.BankSetting.BankAccountsSettings;
using Banks.Tools.BankSetting.BuildingAccountsProperties;

namespace Banks.Services.UI.Commands.Helpers
{
    public class BankSettingsPrinter
    {
        public static void SetCreditAccountSettings(IUserInterface userInterface, CreditAccountSettings settings)
        {
            userInterface.Write("Credit account settings:");
            userInterface.Write($"Lower limit: {settings.LowerLimit}");
            userInterface.Write($"Daily sum commission: {settings.DailySumCommission(0)}");
        }

        public static void SetDebitAccountSettings(IUserInterface userInterface, DebitAccountSettings settings)
        {
            userInterface.Write("Debit account settings:");
            userInterface.Write($"Monthly percent commission: {settings.MonthlyPercentCommission(0)}%");
        }

        public static void SetDepositAccountSettings(IUserInterface userInterface, DepositAccountSettings settings)
        {
            userInterface.Write("Deposit account settings:");
            userInterface.Write($"Days duration: {settings.DaysDuration}");
            List<CommissionLimitForDepositAccount> commissionsSettings = settings.CommissionsSettings;
            userInterface.Write($"{commissionsSettings.Count} limit(s):");
            foreach (CommissionLimitForDepositAccount commissionsSetting in commissionsSettings)
            {
                userInterface.Write($"Less than {commissionsSetting.MaxSum} - {commissionsSetting.PercentMonthCommission}%");
            }
        }
    }
}