using System.Collections.Generic;

namespace Banks.Tools.Transactions
{
    public class TransactionList
    {
        public TransactionList()
        {
            Transactions = new List<Transaction>();
        }

        public List<Transaction> Transactions { get; }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }
    }
}