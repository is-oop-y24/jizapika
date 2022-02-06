using System.Collections.Generic;
using System.Collections.Immutable;

namespace Banks.Tools.Transactions
{
    public class TransactionList
    {
        private List<Transaction> _transactions;
        public TransactionList()
        {
            _transactions = new List<Transaction>();
        }

        public ImmutableList<Transaction> ImmutableTransactions => _transactions.ToImmutableList();

        public void AddTransaction(Transaction transaction)
        {
            if (transaction != null)
                _transactions.Add(transaction);
        }
    }
}