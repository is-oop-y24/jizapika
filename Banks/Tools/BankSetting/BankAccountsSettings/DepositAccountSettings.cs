using System.Collections.Generic;
using System.Linq;
using Banks.Exceptions;
using Banks.Tools.BankSetting.BuildingAccountsProperties;

namespace Banks.Tools.BankSetting.BankAccountsSettings
{
    public class DepositAccountSettings
    {
        private double _currentMax;

        public DepositAccountSettings(uint daysDuration)
        {
            _currentMax = 0;
            CommissionsSettings = new List<CommissionLimitForDepositAccount>();
            DaysDuration = daysDuration;
        }

        public List<CommissionLimitForDepositAccount> CommissionsSettings { get; }
        public uint DaysDuration { get; }

        public void AddCommissionLimit(double raisedBorder, double newCommission)
        {
            if (_currentMax >= raisedBorder)
            {
                throw new BankException($"Small border");
            }

            var commissionLimitForDepositAccount = new CommissionLimitForDepositAccount(raisedBorder, newCommission);
            _currentMax = raisedBorder;
            if (CommissionsSettings.Count > 0) CommissionsSettings.Last().NextObject = commissionLimitForDepositAccount;
            CommissionsSettings.Add(commissionLimitForDepositAccount);
        }

        public double MonthlyPercentCommission(double currentAccountSum)
        {
            if (CommissionsSettings.Count == 0)
                throw new BankException($"DepositSettings aren't.");
            return CommissionsSettings[0].MonthCommission(currentAccountSum);
        }
    }
}