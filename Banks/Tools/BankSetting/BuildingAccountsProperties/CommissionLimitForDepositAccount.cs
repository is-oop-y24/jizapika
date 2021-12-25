using System;
using Banks.Exceptions;

namespace Banks.Tools.BankSetting.BuildingAccountsProperties
{
    public class CommissionLimitForDepositAccount
    {
        public CommissionLimitForDepositAccount(
            double maxSum,
            double monthCommission)
        {
            NextObject = null;
            MaxSum = maxSum;
            PercentMonthCommission = monthCommission;
        }

        public double MaxSum { get; }
        public double PercentMonthCommission { get; }

        public CommissionLimitForDepositAccount NextObject { get; set; }

        public double MonthCommission(double currentAccountSum)
        {
            if (NextObject != null)
                return currentAccountSum > MaxSum ? NextObject.MonthCommission(currentAccountSum) : PercentMonthCommission;
            else
                throw new BankException($"This sum is very big for this account (Banks.Tools.BankSettings.CommissionsForAccounts.CommissionLimitForDepositAccount)");
        }
    }
}