namespace Banks.Tools.BankSetting.BankAccountsSettings
{
    public class DebitAccountSettings
    {
        private double _commission;

        public DebitAccountSettings(double commission)
        {
            _commission = commission;
        }

        public double MonthlyPercentCommission(double currentAccountSum)
            => _commission;
    }
}