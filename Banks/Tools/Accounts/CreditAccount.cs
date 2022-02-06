using System.Collections.Generic;
using Banks.Exceptions;
using Banks.Tools.BankSetting.BankAccountsSettings;
using Banks.Tools.ClientPart;
using Banks.Tools.Transactions;

namespace Banks.Tools.Accounts
{
    public class CreditAccount : Account
    {
        private Client _client;
        private CreditAccountSettings _settings;
        private uint _dateOfCreation;
        private double _accumulatedSum;

        public CreditAccount(CreditAccountSettings settings, Client client, uint id, uint dateOfCreation)
        {
            _client = client;
            _settings = settings;
            _dateOfCreation = dateOfCreation;
            _accumulatedSum = 0;
            Sum = 0;
            Id = id;
        }

        public new uint Id { get; }
        public new double Sum { get; internal set; }

        public override WithdrawalTransaction Withdrawal(double withdrawalSum, uint transactionId)
        {
            if (!_client.IsApproved())
                throw new BankException($"Client isn't approved and cannot make a withdrawal.");
            if (Sum - withdrawalSum < _settings.LowerLimit)
                throw new BankException($"Credit account doesn't have enough money for the transaction.");
            return new IoTransaction(this, transactionId, -withdrawalSum);
        }

        public override ReplenishmentTransaction Replenishment(double replenishmentSum, uint transactionId)
            => new IoTransaction(this, transactionId, replenishmentSum);

        public override TranslationToTransaction TranslationTo(Account otherAccount, double translationSum, uint transactionId)
        {
            if (!_client.IsApproved())
                throw new BankException($"Client isn't approved and cannot make a translation.");
            if (Sum - translationSum < _settings.LowerLimit)
                throw new BankException($"Credit account doesn't have enough money for the transaction.");
            return new ConnectTransaction(this, otherAccount, transactionId, translationSum);
        }

        public override void WaitDay(uint currentDate)
        {
            if (_accumulatedSum < 0) _accumulatedSum -= _settings.DailySumCommission(Sum);
            if ((currentDate - _dateOfCreation) % 30 == 0)
            {
                Sum += _accumulatedSum;
                _accumulatedSum = 0;
            }
        }

        public override TransactionList AccountTransactions(AllTransactions allTransactions)
        {
            var transactions = new TransactionList();
            foreach (Transaction transaction in allTransactions.ImmutableTransactions)
                if (transaction.IsAccountId(Id)) transactions.AddTransaction(transaction);
            return transactions;
        }

        public override bool IsClientId(uint id)
            => _client.Id == id;

        public override string Type()
            => "credit";
    }
}