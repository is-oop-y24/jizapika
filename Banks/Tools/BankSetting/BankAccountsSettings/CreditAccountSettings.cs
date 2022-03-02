using Banks.Exceptions;

namespace Banks.Tools.BankSetting.BankAccountsSettings
{
    public class CreditAccountSettings
    {
        private double _dailySumCommission;
        public CreditAccountSettings(double lowerLimit, double dailySumCommission)
        {
            if (lowerLimit >= 0)
                throw new BankException($"Lower limit for credit account need to be negative.");
            LowerLimit = lowerLimit;
            _dailySumCommission = dailySumCommission;
        }

        public double LowerLimit { get; }

        public double DailySumCommission(double currentAccountSum)
            => _dailySumCommission;
    }
}