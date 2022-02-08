using Banks.Exceptions;
using Banks.Tools;
using Banks.Tools.Accounts;
using Banks.Tools.Banks;
using Banks.Tools.BankSetting;
using Banks.Tools.BankSetting.BankAccountsSettings;
using Banks.Tools.ClientPart;
using NUnit.Framework;

namespace Banks.Tests
{
    [TestFixture]
    public class BackUpsTest
    {
        [Test]
        public void SumChangingTesting()
        {
            var centralBank = new CentralBank();
            var typicalCreditAccountSettings = new CreditAccountSettings(-40000, 100);
            var typicalDebitAccountSettings = new DebitAccountSettings(0.003);
            var typicalDepositAccountSettings = new DepositAccountSettings(100);
            typicalDepositAccountSettings.AddCommissionLimit(5000, 0.0035);
            typicalDepositAccountSettings.AddCommissionLimit(50000, 0.004);
            typicalDepositAccountSettings.AddCommissionLimit(5000000, 0.0045);
            var typicalBankSettings = new BankSettings(typicalCreditAccountSettings, typicalDebitAccountSettings,
                typicalDepositAccountSettings);
            Bank sberbank = centralBank.AddBank(typicalBankSettings, "Sberbank");
            Client lev = centralBank.AddClient(sberbank, "Lev", "Chechulin");
            lev.Address = "Vyazemskiy";
            lev.Passport = "228337";
            CreditAccount credit = centralBank.AddCreditAccount(lev);
            DebitAccount debit = centralBank.AddDebitAccount(lev);
            DepositAccount deposit = centralBank.AddDepositAccount(lev);
            credit.MakeWithdrawal(1000);
            debit.MakeReplenishment(50000);
            deposit.MakeReplenishment(4970);
            centralBank.AddDays(120);
            Assert.AreEqual(-1000 - 120 * 100, credit.Sum, 0.0001);
            Assert.AreEqual(50000 * 1.003 * 1.003 * 1.003 * 1.003, debit.Sum, 0.0001);
            Assert.AreEqual(4970 * 1.0035 * 1.0035 * 1.004 * (1 + 0.004 / 30 * 10), deposit.Sum, 0.0001);
        }

        [Test]
        public void NotNormalCreditWithdrawaling()
        {
            Assert.Catch<BankException>(() =>
            {
                var centralBank = new CentralBank();
                var typicalCreditAccountSettings = new CreditAccountSettings(-40000, 100);
                var typicalDebitAccountSettings = new DebitAccountSettings(0.003);
                var typicalDepositAccountSettings = new DepositAccountSettings(100);
                typicalDepositAccountSettings.AddCommissionLimit(5000, 0.0035);
                typicalDepositAccountSettings.AddCommissionLimit(50000, 0.004);
                typicalDepositAccountSettings.AddCommissionLimit(5000000, 0.0045);
                var typicalBankSettings = new BankSettings(typicalCreditAccountSettings, typicalDebitAccountSettings,
                    typicalDepositAccountSettings);
                Bank sberbank = centralBank.AddBank(typicalBankSettings, "Sberbank");
                Client lev = centralBank.AddClient(sberbank, "Lev", "Chechulin");
                lev.Address = "Vyazemskiy";
                lev.Passport = "228337";
                CreditAccount credit = centralBank.AddCreditAccount(lev);
                credit.MakeWithdrawal(100000);
            });
        }

        [Test]
        public void NotNormalDebitSum()
        {
            Assert.Catch<BankException>(() =>
            {
                var centralBank = new CentralBank();
                var typicalCreditAccountSettings = new CreditAccountSettings(-40000, 100);
                var typicalDebitAccountSettings = new DebitAccountSettings(0.003);
                var typicalDepositAccountSettings = new DepositAccountSettings(100);
                typicalDepositAccountSettings.AddCommissionLimit(5000, 0.0035);
                typicalDepositAccountSettings.AddCommissionLimit(50000, 0.004);
                typicalDepositAccountSettings.AddCommissionLimit(5000000, 0.0045);
                var typicalBankSettings = new BankSettings(typicalCreditAccountSettings, typicalDebitAccountSettings,
                    typicalDepositAccountSettings);
                Bank sberbank = centralBank.AddBank(typicalBankSettings, "Sberbank");
                Client lev = centralBank.AddClient(sberbank, "Lev", "Chechulin");
                lev.Address = "Vyazemskiy";
                lev.Passport = "228337";
                DebitAccount debit = centralBank.AddDebitAccount(lev);
                debit.MakeWithdrawal(1);
            });
        }

        [Test]
        public void DepositWithdrawaling()
        {
            Assert.Catch<BankException>(() =>
            {
                var centralBank = new CentralBank();
                var typicalCreditAccountSettings = new CreditAccountSettings(-40000, 100);
                var typicalDebitAccountSettings = new DebitAccountSettings(0.003);
                var typicalDepositAccountSettings = new DepositAccountSettings(100);
                typicalDepositAccountSettings.AddCommissionLimit(5000, 0.0035);
                typicalDepositAccountSettings.AddCommissionLimit(50000, 0.004);
                typicalDepositAccountSettings.AddCommissionLimit(5000000, 0.0045);
                var typicalBankSettings = new BankSettings(typicalCreditAccountSettings, typicalDebitAccountSettings,
                    typicalDepositAccountSettings);
                Bank sberbank = centralBank.AddBank(typicalBankSettings, "Sberbank");
                Client lev = centralBank.AddClient(sberbank, "Lev", "Chechulin");
                lev.Address = "Vyazemskiy";
                lev.Passport = "228337";
                DepositAccount deposit = centralBank.AddDepositAccount(lev);
                deposit.MakeReplenishment(100000);
                deposit.MakeWithdrawal(1);
            });
        }

        [Test]
        public void Translations()
        {
            var centralBank = new CentralBank();
            var typicalCreditAccountSettings = new CreditAccountSettings(-40000, 100);
            var typicalDebitAccountSettings = new DebitAccountSettings(0.003);
            var typicalDepositAccountSettings = new DepositAccountSettings(100);
            typicalDepositAccountSettings.AddCommissionLimit(5000, 0.0035);
            typicalDepositAccountSettings.AddCommissionLimit(50000, 0.004);
            typicalDepositAccountSettings.AddCommissionLimit(5000000, 0.0045);
            var typicalBankSettings = new BankSettings(typicalCreditAccountSettings, typicalDebitAccountSettings,
                typicalDepositAccountSettings);
            Bank sberbank = centralBank.AddBank(typicalBankSettings, "Sberbank");
            Client lev = centralBank.AddClient(sberbank, "Lev", "Chechulin");
            lev.Address = "Vyazemskiy";
            lev.Passport = "228337";
            CreditAccount creditLev = centralBank.AddCreditAccount(lev);
            DebitAccount debitLev = centralBank.AddDebitAccount(lev);
            debitLev.MakeReplenishment(50000);
            Client timur = centralBank.AddClient(sberbank, "Timur", "Syrma");
            timur.Address = "Vyazemskiy";
            timur.Passport = "337228";
            CreditAccount creditTimur = centralBank.AddCreditAccount(timur);
            DepositAccount debitTimur = centralBank.AddDepositAccount(timur);
            creditTimur.MakeReplenishment(50000);
            debitTimur.MakeReplenishment(50000);
            creditLev.MakeTranslationTo(creditTimur, 30000);
            debitLev.MakeTranslationTo(debitTimur, 30000);
            Assert.AreEqual(0 - 30000, creditLev.Sum, 0.0001);
            Assert.AreEqual(50000 - 30000, debitLev.Sum, 0.0001);
            Assert.AreEqual(50000 + 30000, creditTimur.Sum, 0.0001);
            Assert.AreEqual(50000 + 30000, debitTimur.Sum, 0.0001);
        }

        [Test]
        public void CreditWithdrawalWithNotApprovedClient()
        {
            Assert.Catch<BankException>(() =>
            {
                var centralBank = new CentralBank();
                var typicalCreditAccountSettings = new CreditAccountSettings(-40000, 100);
                var typicalDebitAccountSettings = new DebitAccountSettings(0.003);
                var typicalDepositAccountSettings = new DepositAccountSettings(100);
                typicalDepositAccountSettings.AddCommissionLimit(5000, 0.0035);
                typicalDepositAccountSettings.AddCommissionLimit(50000, 0.004);
                typicalDepositAccountSettings.AddCommissionLimit(5000000, 0.0045);
                var typicalBankSettings = new BankSettings(typicalCreditAccountSettings, typicalDebitAccountSettings,
                    typicalDepositAccountSettings);
                Bank sberbank = centralBank.AddBank(typicalBankSettings, "Sberbank");
                Client lev = centralBank.AddClient(sberbank, "Lev", "Chechulin");
                CreditAccount credit = centralBank.AddCreditAccount(lev);
                credit.MakeWithdrawal(1000);
            });
            ;
        }

        [Test]
        public void DebitWithdrawalWithNotApprovedClient()
        {
            Assert.Catch<BankException>(() =>
            {
                var centralBank = new CentralBank();
                var typicalCreditAccountSettings = new CreditAccountSettings(-40000, 100);
                var typicalDebitAccountSettings = new DebitAccountSettings(0.003);
                var typicalDepositAccountSettings = new DepositAccountSettings(100);
                typicalDepositAccountSettings.AddCommissionLimit(5000, 0.0035);
                typicalDepositAccountSettings.AddCommissionLimit(50000, 0.004);
                typicalDepositAccountSettings.AddCommissionLimit(5000000, 0.0045);
                var typicalBankSettings = new BankSettings(typicalCreditAccountSettings, typicalDebitAccountSettings,
                    typicalDepositAccountSettings);
                Bank sberbank = centralBank.AddBank(typicalBankSettings, "Sberbank");
                Client lev = centralBank.AddClient(sberbank, "Lev", "Chechulin");
                DebitAccount debit = centralBank.AddDebitAccount(lev);
                debit.MakeWithdrawal(1000);
            });;
        }

        [Test]
        public void DepositWithdrawalWithNotApprovedClient()
        {
            Assert.Catch<BankException>(() =>
            {
                var centralBank = new CentralBank();
                var typicalCreditAccountSettings = new CreditAccountSettings(-40000, 100);
                var typicalDebitAccountSettings = new DebitAccountSettings(0.003);
                var typicalDepositAccountSettings = new DepositAccountSettings(100);
                typicalDepositAccountSettings.AddCommissionLimit(5000, 0.0035);
                typicalDepositAccountSettings.AddCommissionLimit(50000, 0.004);
                typicalDepositAccountSettings.AddCommissionLimit(5000000, 0.0045);
                var typicalBankSettings = new BankSettings(typicalCreditAccountSettings, typicalDebitAccountSettings,
                    typicalDepositAccountSettings);
                Bank sberbank = centralBank.AddBank(typicalBankSettings, "Sberbank");
                Client lev = centralBank.AddClient(sberbank, "Lev", "Chechulin");
                DepositAccount deposit = centralBank.AddDepositAccount(lev);
                deposit.MakeWithdrawal(1000);
            });;
        }
    }
}