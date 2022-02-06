using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Transactions;
using Banks.Exceptions;
using Banks.Tools.Accounts;

namespace Banks.Tools.Transactions
{
    public class AllTransactions
    {
        private List<Transaction> _transactions;
        private uint _currentNumberId = 1;

        public AllTransactions()
        {
            _transactions = new List<Transaction>();
        }

        public ImmutableList<Transaction> ImmutableTransactions => _transactions.ToImmutableList();

        public IoTransaction AddWithdrawalTransaction(Account account, double sum)
        {
            IoTransaction transaction = account.Withdrawal(sum, _currentNumberId);
            _transactions.Add(transaction);
            _currentNumberId++;
            return transaction;
        }

        public IoTransaction AddReplenishmentTransaction(Account account, double sum)
        {
            IoTransaction transaction = account.Replenishment(sum, _currentNumberId);
            _transactions.Add(transaction);
            _currentNumberId++;
            return transaction;
        }

        public ConnectTransaction AddTranslationTransaction(Account from, Account to, double sum)
        {
            ConnectTransaction transaction = from.TranslationTo(to, sum, _currentNumberId);
            _transactions.Add(transaction);
            _currentNumberId++;
            return transaction;
        }

        public Transaction FindTransaction(uint id)
            => _transactions.FirstOrDefault(transaction => transaction.Id == id);
    }
}