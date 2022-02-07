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

        public WithdrawalTransaction AddWithdrawalTransaction(Account account, double sum)
        {
            var transaction = new WithdrawalTransaction(account, _currentNumberId, sum);
            _transactions.Add(transaction);
            _currentNumberId++;
            return transaction;
        }

        public ReplenishmentTransaction AddReplenishmentTransaction(Account account, double sum)
        {
            var transaction = new ReplenishmentTransaction(account, _currentNumberId, sum);
            _transactions.Add(transaction);
            _currentNumberId++;
            return transaction;
        }

        public TranslationTransaction AddTranslationTransaction(Account from, Account to, double sum)
        {
            var transaction = new TranslationTransaction(from, to, _currentNumberId, sum);
            _transactions.Add(transaction);
            _currentNumberId++;
            return transaction;
        }

        public Transaction FindTransaction(uint id)
            => _transactions.FirstOrDefault(transaction => transaction.Id == id);
    }
}