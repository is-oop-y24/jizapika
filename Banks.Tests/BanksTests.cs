using Banks.Exceptions;
using Banks.Tools.BankSetting;
using Banks.Tools.BankSetting.BankAccountsSettings;
using Banks.Tools.CentralBankTools;
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
            var typicalBankSettings = new BankSettings(typicalCreditAccountSettings, typicalDebitAccountSettings, typicalDepositAccountSettings);
            uint sberbankId = centralBank.AddBank_ReturnID(typicalBankSettings, "Sberbank");
            uint levId = centralBank.AddClient_ReturnID(sberbankId, "Lev", "Chechulin");
            centralBank.ReaddressClient(levId, "Vyazemskiy");
            centralBank.RepassportClient(levId, "228337");
            uint creditAccountId = centralBank.AddCreditAccount_ReturnID(levId);
            uint debitAccountId = centralBank.AddDebitAccount_ReturnID(levId);
            uint depositAccountId = centralBank.AddDepositAccount_ReturnID(levId);
            centralBank.MakeNewWithdrawal(creditAccountId, 1000);
            centralBank.MakeNewReplenishment(debitAccountId, 50000);
            centralBank.MakeNewReplenishment(depositAccountId, 4970);
            centralBank.AddDays(120);
            Assert.AreEqual(-1000 - 120 * 100, centralBank.AccountSum(creditAccountId), 0.0001);
            Assert.AreEqual(50000 * 1.003 * 1.003 * 1.003 * 1.003, centralBank.AccountSum(debitAccountId), 0.0001);
            Assert.AreEqual(4970 * 1.0035 * 1.0035 * 1.004 * (1 + 0.004 / 30 * 10), centralBank.AccountSum(depositAccountId), 0.0001);
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
                uint sberbankId = centralBank.AddBank_ReturnID(typicalBankSettings, "Sberbank");
                uint levId = centralBank.AddClient_ReturnID(sberbankId, "Lev", "Chechulin");
                centralBank.ReaddressClient(levId, "Vyazemskiy");
                centralBank.RepassportClient(levId, "228337");
                uint creditAccountId = centralBank.AddCreditAccount_ReturnID(levId);
                centralBank.MakeNewWithdrawal(creditAccountId, 100000);
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
                uint sberbankId = centralBank.AddBank_ReturnID(typicalBankSettings, "Sberbank");
                uint levId = centralBank.AddClient_ReturnID(sberbankId, "Lev", "Chechulin");
                centralBank.ReaddressClient(levId, "Vyazemskiy");
                centralBank.RepassportClient(levId, "228337");
                uint debitAccountId = centralBank.AddDebitAccount_ReturnID(levId);
                centralBank.MakeNewWithdrawal(debitAccountId, 1);
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
                uint sberbankId = centralBank.AddBank_ReturnID(typicalBankSettings, "Sberbank");
                uint levId = centralBank.AddClient_ReturnID(sberbankId, "Lev", "Chechulin");
                centralBank.ReaddressClient(levId, "Vyazemskiy");
                centralBank.RepassportClient(levId, "228337");
                uint depositAccountId = centralBank.AddDepositAccount_ReturnID(levId);
                centralBank.MakeNewReplenishment(depositAccountId, 100000);
                centralBank.MakeNewWithdrawal(depositAccountId, 1);
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
            var typicalBankSettings = new BankSettings(typicalCreditAccountSettings, typicalDebitAccountSettings, typicalDepositAccountSettings);
            uint sberbankId = centralBank.AddBank_ReturnID(typicalBankSettings, "Sberbank");
            uint levId = centralBank.AddClient_ReturnID(sberbankId, "Lev", "Chechulin");
            centralBank.ReaddressClient(levId, "Vyazemskiy");
            centralBank.RepassportClient(levId, "228337");
            uint creditLevId = centralBank.AddCreditAccount_ReturnID(levId);
            uint debitLevId = centralBank.AddDebitAccount_ReturnID(levId);
            centralBank.MakeNewReplenishment(debitLevId, 50000);
            uint timurId = centralBank.AddClient_ReturnID(sberbankId, "Timur", "Syrma");
            centralBank.ReaddressClient(timurId, "Vyazemskiy");
            centralBank.RepassportClient(timurId, "228337");
            uint creditTimurId = centralBank.AddCreditAccount_ReturnID(timurId);
            uint debitTimurId = centralBank.AddDebitAccount_ReturnID(timurId);
            centralBank.MakeNewReplenishment(creditTimurId, 50000);
            centralBank.MakeNewReplenishment(debitTimurId, 50000);
            centralBank.MakeNewTranslation(creditLevId, creditTimurId, 30000);
            centralBank.MakeNewTranslation(debitLevId, debitTimurId, 30000);
            Assert.AreEqual(0 - 30000, centralBank.AccountSum(creditLevId), 0.0001);
            Assert.AreEqual(50000 - 30000, centralBank.AccountSum(debitLevId), 0.0001);
            Assert.AreEqual(50000 + 30000, centralBank.AccountSum(creditTimurId), 0.0001);
            Assert.AreEqual(50000 + 30000, centralBank.AccountSum(debitTimurId), 0.0001);
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
                uint sberbankId = centralBank.AddBank_ReturnID(typicalBankSettings, "Sberbank");
                uint levId = centralBank.AddClient_ReturnID(sberbankId, "Lev", "Chechulin");
                uint creditAccountId = centralBank.AddCreditAccount_ReturnID(levId);
                centralBank.MakeNewWithdrawal(creditAccountId, 1000);
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
                uint sberbankId = centralBank.AddBank_ReturnID(typicalBankSettings, "Sberbank");
                uint levId = centralBank.AddClient_ReturnID(sberbankId, "Lev", "Chechulin");
                uint debitAccountId = centralBank.AddDebitAccount_ReturnID(levId);
                centralBank.MakeNewWithdrawal(debitAccountId, 1000);
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
                uint sberbankId = centralBank.AddBank_ReturnID(typicalBankSettings, "Sberbank");
                uint levId = centralBank.AddClient_ReturnID(sberbankId, "Lev", "Chechulin");
                uint depositAccountId = centralBank.AddDepositAccount_ReturnID(levId);
                centralBank.MakeNewWithdrawal(depositAccountId, 1000);
            });;
        }
    }
}