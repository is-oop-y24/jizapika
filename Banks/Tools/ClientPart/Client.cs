using Banks.Exceptions;
using Banks.Tools.Accounts;
using Banks.Tools.Banks;
using Banks.Tools.Transactions;

namespace Banks.Tools.ClientPart
{
    public class Client
    {
        private bool _isBlocked;

        public Client(Bank bank, uint id, string name, string surname)
        {
            Bank = bank;
            _isBlocked = false;
            Id = id;
            Name = name;
            Surname = surname;
        }

        public Bank Bank { get; }
        public uint Id { get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Passport { get; set; }
        public string Address { get; set; }

        public bool IsApproved()
            => !_isBlocked || !(string.IsNullOrEmpty(Passport) || string.IsNullOrEmpty(Address));

        public AccountList ClientAccounts(AllAccounts allAccounts)
        {
            var accounts = new AccountList();
            foreach (Account account in allAccounts.ImmutableAccounts)
                if (account.IsClientId(Id)) accounts.AddAccount(account);

            return accounts;
        }

        public bool IsBankId(uint id)
            => Bank.Id == id;

        public void BlockHim(AllAccounts allAccounts, AllTransactions allTransactions)
        {
            if (!_isBlocked)
                return;
            CancelAllTransaction(allAccounts, allTransactions);
            _isBlocked = true;
        }

        public void UnBlockHim(AllAccounts allAccounts, AllTransactions allTransactions)
        {
            if (_isBlocked)
                return;
            UnCancelAllTransaction(allAccounts, allTransactions);
            _isBlocked = false;
        }

        private void CancelAllTransaction(AllAccounts allAccounts, AllTransactions allTransactions)
        {
            AccountList accounts = ClientAccounts(allAccounts);
            foreach (Account account in accounts.Accounts)
            {
                TransactionList transactions = account.AccountTransactions(allTransactions);
                foreach (Transaction transaction in transactions.Transactions)
                {
                    transaction.Cancel();
                }
            }
        }

        private void UnCancelAllTransaction(AllAccounts allAccounts, AllTransactions allTransactions)
        {
            AccountList accounts = ClientAccounts(allAccounts);
            foreach (Account account in accounts.Accounts)
            {
                TransactionList transactions = account.AccountTransactions(allTransactions);
                foreach (Transaction transaction in transactions.Transactions)
                {
                    transaction.UnCancel();
                }
            }
        }
    }
}