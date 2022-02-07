using Banks.Tools.Accounts;
using Banks.Tools.Banks;
using Banks.Tools.BankSetting;
using Banks.Tools.ClientPart;
using Banks.Tools.Transactions;

namespace Banks.Tools
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

        public AllBanks Banks { get; }
        public AllClients Clients { get; }
        public AllAccounts Accounts { get; }
        public AllTransactions Transactions { get; }

        public Bank AddBank(BankSettings settings, string name)
            => Banks.AddBank(settings, name);

        public Client AddClient(Bank bank, string name, string surname)
            => Clients.AddClient(bank, name, surname);

        public Account AddCreditAccount(Client client)
            => Accounts.AddCreditAccount(client.Bank.Settings.CreditSettings, client, _currentDay);

        public Account AddDebitAccount(Client client)
            => Accounts.AddDebitAccount(client.Bank.Settings.DebitSettings, client, _currentDay);

        public Account AddDepositAccount(Client client)
            => Accounts.AddDepositAccount(client.Bank.Settings.DepositSettings, client, _currentDay);

        public WithdrawalTransaction AddWithdrawal(Account account, double sum)
            => Transactions.AddWithdrawalTransaction(account, sum);
        public ReplenishmentTransaction AddReplenishment(Account account, double sum)
            => Transactions.AddReplenishmentTransaction(account, sum);
        public TranslationTransaction AddTranslation(Account fromAccount, Account toAccount, double sum)
            => Transactions.AddTranslationTransaction(fromAccount, toAccount, sum);

        public void MakeTransaction(Transaction transaction)
        {
            transaction.MakeIt();
        }

        public void AddDays(uint days)
        {
            for (int d = 0; d < days; d++) WaitOneDay();
        }

        public Client RenameClient(uint id, string newName)
            => Clients.RenameClient(id, newName);

        public Client ResurnameClient(uint id, string newSurname)
            => Clients.ResurnameClient(id, newSurname);

        public Client RepassportClient(uint id, string newPassport)
            => Clients.RepassportClient(id, newPassport);

        public Client ReaddressClient(uint id, string newAddress)
            => Clients.ReaddressClient(id, newAddress);

        public Bank RenameBank(uint id, string newName)
            => Banks.RenameBank(id, newName);

        public Client BlockClient(uint id)
            => Clients.BlockClient(id, Accounts, Transactions);

        private void WaitOneDay()
        {
            Accounts.WaitDay(_currentDay);
            _currentDay++;
        }
    }
}