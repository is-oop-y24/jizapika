using Banks.Tools.BankSetting.BankAccountsSettings;

namespace Banks.Tools.BankSetting
{
    public class BankSettings
    {
        public BankSettings(
            CreditAccountSettings creditSettings, DebitAccountSettings debitSettings, DepositAccountSettings depositSettings)
        {
            CreditSettings = creditSettings;
            DebitSettings = debitSettings;
            DepositSettings = depositSettings;
        }

        public CreditAccountSettings CreditSettings { get; set; }
        public DebitAccountSettings DebitSettings { get; set; }
        public DepositAccountSettings DepositSettings { get; set; }
    }
}