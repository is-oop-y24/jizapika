using System;
using Banks.Exceptions;
using Banks.Tools;
using Banks.Tools.BankSetting.BankAccountsSettings;

namespace Banks.Services.UI.Commands.Helpers
{
    public class BankSettingsHelper
    {
        public static CreditAccountSettings CreateCreditAccountSettings(IUserInterface userInterface)
        {
            userInterface.Write("Let's create credit account settings.");

            double lowerLimit = double.MaxValue;
            while (lowerLimit >= 0)
            {
                try
                {
                    lowerLimit =
                        double.Parse(userInterface.WriteAndRead("Enter lower limit of this account. " +
                                                                "This number must be negative: "));
                }
                catch (Exception)
                {
                    userInterface.Write("Parse failed. Try again.");
                }
            }

            double dailySumCommission = double.MinValue;
            while (dailySumCommission < 0)
            {
                try
                {
                    dailySumCommission =
                        double.Parse(userInterface.WriteAndRead(
                            "Enter daily sum commission of this account. " +
                            "The number should show the ammount debited daily if the invoice amount is negative: "));
                }
                catch (Exception)
                {
                    userInterface.Write("Parse failed. Try again.");
                }
            }

            return new CreditAccountSettings(lowerLimit, dailySumCommission);
        }

        public static DebitAccountSettings CreateDebitAccountSettings(IUserInterface userInterface)
        {
            userInterface.Write("Let's create debit account settings.");

            double monthlyCommission = double.MinValue;
            while (monthlyCommission < 0)
            {
                try
                {
                    monthlyCommission =
                        double.Parse(userInterface.WriteAndRead(
                            "Enter percent monthly commission of this account."));
                }
                catch (Exception)
                {
                    userInterface.Write("Parse failed. Try again.");
                }
            }

            return new DebitAccountSettings(monthlyCommission);
        }

        public static DepositAccountSettings CreateDepositAccountSettings(IUserInterface userInterface)
        {
            userInterface.Write("Let's create deposit account settings.");

            uint daysDuration = 0;
            while (daysDuration == 0)
            {
                try
                {
                    daysDuration =
                        uint.Parse(userInterface.WriteAndRead(
                            "Please enter how many days will during deposit account: "));
                }
                catch (Exception)
                {
                    userInterface.Write("Parse failed. Try again.");
                }
            }

            userInterface.Write("Nice, let's set the number of limits");

            int numberLimits = int.MinValue;
            while (numberLimits < 0)
            {
                try
                {
                    numberLimits =
                        int.Parse(userInterface.WriteAndRead(
                            "Please enter the number of limits:"));
                }
                catch (Exception)
                {
                    userInterface.Write("Parse failed. Try again.");
                }
            }

            userInterface.Write("Nice, let's set these limits");
            var settings = new DepositAccountSettings(daysDuration);
            for (int i = 1; i <= numberLimits; i++)
            {
                userInterface.Write($"{i} limit)");

                double maxSum = double.MinValue;
                double monthCommission = double.MinValue;
                bool flagForAdding = true;
                while (flagForAdding)
                {
                    while (maxSum <= 0 || monthCommission <= 0)
                    {
                        try
                        {
                            maxSum =
                                double.Parse(userInterface.WriteAndRead(
                                    "Please enter the maximal sum on this limit (more than previous): "));
                            monthCommission =
                                double.Parse(userInterface.WriteAndRead(
                                    "Please enter the month commission on this limit: "));
                        }
                        catch (Exception)
                        {
                            maxSum = double.MinValue;
                            monthCommission = double.MinValue;
                            userInterface.Write("Parse failed. Please, try again.");
                        }

                        try
                        {
                            settings.AddCommissionLimit(maxSum, monthCommission);
                            flagForAdding = false;
                        }
                        catch (Exception)
                        {
                            maxSum = double.MinValue;
                            monthCommission = double.MinValue;
                            userInterface.Write("Something wasn't correct. Please, try again.");
                        }
                    }
                }
            }

            return settings;
        }
    }
}