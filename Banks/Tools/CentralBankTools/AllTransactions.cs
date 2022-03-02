using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Banks.Exceptions;
using Banks.Tools.Accounts;
using Banks.Tools.Transactions;

namespace Banks.Tools.CentralBankTools
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

        public uint AddWithdrawalTransaction_ReturnID(Account account, double sum)
        {
            var transaction = new WithdrawalTransaction(account, _currentNumberId, sum);
            _transactions.Add(transaction);
            _currentNumberId++;
            return transaction.Id;
        }

        public uint AddReplenishmentTransaction_ReturnID(Account account, double sum)
        {
            var transaction = new ReplenishmentTransaction(account, _currentNumberId, sum);
            _transactions.Add(transaction);
            _currentNumberId++;
            return transaction.Id;
        }

        public uint AddTranslationTransaction_ReturnID(Account from, Account to, double sum)
        {
            var transaction = new TranslationTransaction(from, to, _currentNumberId, sum);
            _transactions.Add(transaction);
            _currentNumberId++;
            return transaction.Id;
        }

        public Transaction FindTransaction(uint id)
        {
            if (!IsCorrectTransactionId(id))
                throw new BankException("Transaction doesn't exist.");
            return _transactions.FirstOrDefault(transaction => transaction.Id == id);
        }

        public bool IsCorrectTransactionId(uint transactionId)
            => _transactions.Any(transaction => transaction.Id == transactionId);
    }
}