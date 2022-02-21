using System.Collections.Immutable;
using Banks.Tools.Accounts;
using Banks.Tools.Banks;
using Banks.Tools.BankSetting;
using Banks.Tools.BankSetting.BankAccountsSettings;
using Banks.Tools.ClientPart;
using Banks.Tools.Transactions;

namespace Banks.Tools.CentralBankTools
{
    public class CentralBank
    {
        private uint _currentDay;
        public CentralBank()
        {
            Banks = new AllBanks();
            Clients = new AllClients();
            Accounts = new AllAccounts();
            Transactions = new AllTransactions();
            _currentDay = 0;
        }

        public ImmutableList<Bank> AllBanks => Banks.ImmutableBanks;
        private AllBanks Banks { get; }
        private AllClients Clients { get; }
        private AllAccounts Accounts { get; }
        private AllTransactions Transactions { get; }
        public uint AddBank_ReturnID(BankSettings settings, string name) => Banks.AddBank_ReturnID(settings, name);
        public uint AddClient_ReturnID(uint bankId, string name, string surname)
            => Clients.AddClient_ReturnID(Banks.FindBank(bankId), name, surname);
        public uint AddCreditAccount_ReturnID(uint clientId) => Accounts.AddCreditAccount_ReturnID(
                Clients.FindClient(clientId).Bank.Settings.CreditSettings, Clients.FindClient(clientId), _currentDay);
        public uint AddDebitAccount_ReturnID(uint clientId) => Accounts.AddDebitAccount_ReturnID(
                Clients.FindClient(clientId).Bank.Settings.DebitSettings, Clients.FindClient(clientId), _currentDay);
        public uint AddDepositAccount_ReturnID(uint clientId) => Accounts.AddDepositAccount_ReturnID(
                Clients.FindClient(clientId).Bank.Settings.DepositSettings, Clients.FindClient(clientId), _currentDay);
        public uint AddWithdrawal_ReturnID(uint accountId, double sum)
            => Transactions.AddWithdrawalTransaction_ReturnID(Accounts.FindAccount(accountId), sum);
        public uint AddReplenishment_ReturnID(uint accountId, double sum)
            => Transactions.AddReplenishmentTransaction_ReturnID(Accounts.FindAccount(accountId), sum);
        public uint AddTranslation_ReturnID(uint fromAccountId, uint toAccountId, double sum)
            => Transactions.AddTranslationTransaction_ReturnID(
                Accounts.FindAccount(fromAccountId), Accounts.FindAccount(toAccountId), sum);
        public void MakeNewWithdrawal(uint accountId, double sum)
            => MakeTransactionById(AddWithdrawal_ReturnID(accountId, sum));
        public void MakeNewReplenishment(uint accountId, double sum)
            => MakeTransactionById(AddReplenishment_ReturnID(accountId, sum));
        public void MakeNewTranslation(uint fromAccountId, uint toAccountId, double sum)
            => MakeTransactionById(AddTranslation_ReturnID(fromAccountId, toAccountId, sum));
        public void MakeTransactionById(uint transactionId) => Transactions.FindTransaction(transactionId).MakeIt();
        public void AddDays(uint days)
        {
            for (int d = 0; d < days; d++) WaitOneDay();
        }

        public bool IsCorrectBankId(uint bankId) => Banks.IsCorrectBankId(bankId);
        public bool IsCorrectClientId(uint clientId) => Clients.IsCorrectClientId(clientId);
        public bool IsCorrectAccountId(uint accountId) => Accounts.IsCorrectAccountId(accountId);
        public bool IsCorrectTransactionId(uint transactionId) => Transactions.IsCorrectTransactionId(transactionId);
        public void RenameBank(uint bankId, string newName) => Banks.RenameBank(bankId, newName);
        public void RenameClient(uint clientId, string newName) => Clients.RenameClient(clientId, newName);
        public void ResurnameClient(uint clientId, string newSurname) => Clients.ResurnameClient(clientId, newSurname);
        public void RepassportClient(uint clientId, string newPassport) => Clients.RepassportClient(clientId, newPassport);
        public void ReaddressClient(uint clientId, string newAddress) => Clients.ReaddressClient(clientId, newAddress);
        public void BlockClient(uint clientId) => Clients.BlockClient(clientId, Accounts, Transactions);
        public string BankName(uint bankId) => Banks.FindBank(bankId).Name;
        public string ClientName(uint clientId) => Clients.FindClient(clientId).Name;
        public string ClientSurname(uint clientId) => Clients.FindClient(clientId).Surname;
        public string ClientPassport(uint clientId) => Clients.FindClient(clientId).Passport;
        public string ClientAddress(uint clientId) => Clients.FindClient(clientId).Address;
        public bool IsClientApproved(uint clientId) => Clients.FindClient(clientId).IsApproved();
        public AccountType AccountType(uint accountId) => Accounts.FindAccount(accountId).Type();
        public double AccountSum(uint accountId) => Accounts.FindAccount(accountId).Sum;
        public TransactionType TransactionType(uint transactionId) => Transactions.FindTransaction(transactionId).Type();
        public double TransactionAmmount(uint transactionId) => Transactions.FindTransaction(transactionId).Ammount;
        public ClientList BankClients(uint bankId) => Banks.FindBank(bankId).BankClients(Clients);
        public AccountList ClientAccounts(uint clientId) => Clients.FindClient(clientId).ClientAccounts(Accounts);
        public TransactionList AccountTransactions(uint accountId) => Accounts.FindAccount(accountId).AccountTransactions(Transactions);
        public CreditAccountSettings GetCreditAccountSettingsByBankId(uint bankId) =>
            Banks.FindBank(bankId).Settings.CreditSettings;
        public DebitAccountSettings GetDebitAccountSettingsByBankId(uint bankId) =>
            Banks.FindBank(bankId).Settings.DebitSettings;
        public DepositAccountSettings GetDepositAccountSettingsByBankId(uint bankId) =>
            Banks.FindBank(bankId).Settings.DepositSettings;
        private void WaitOneDay()
        {
            _currentDay++;
            Accounts.WaitDay(_currentDay);
        }
    }
}