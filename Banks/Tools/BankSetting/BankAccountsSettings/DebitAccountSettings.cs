namespace Banks.Tools.BankSetting.BankAccountsSettings
{
    public class DebitAccountSettings
    {
        private double _monthlycommission;

        public DebitAccountSettings(double commission)
        {
            _monthlycommission = commission;
        }

        public double MonthlyPercentCommission(double currentAccountSum)
            => _monthlycommission;
    }
}