using Banks.Exceptions;
using Banks.Tools.BankSetting.BankAccountsSettings;
using Banks.Tools.ClientPart;
using Banks.Tools.Transactions;

namespace Banks.Tools.Accounts
{
    public class DebitAccount : Account
    {
        private Client _client;
        private DebitAccountSettings _settings;
        private uint _dateOfCreation;
        private double _accumulatedSum;

        public DebitAccount(DebitAccountSettings settings, Client client, uint id, uint dateOfCreation)
        {
            _client = client;
            _settings = settings;
            _dateOfCreation = dateOfCreation;
            _accumulatedSum = 0;
            Sum = 0;
            Id = Id;
        }

        public new double Sum { get; internal set; }
        public new uint Id { get; }

        public override void MakeWithdrawal(double withdrawalSum)
        {
            if (!_client.IsApproved())
                throw new BankException($"Client isn't approved and cannot make a withdrawal.");
            if (withdrawalSum > Sum)
                throw new BankException($"Debit account doesn't have enough money for the transaction.");
            Sum -= withdrawalSum;
        }

        public override void MakeReplenishment(double replenishmentSum)
        {
            Sum += replenishmentSum;
        }

        public override void MakeTranslationTo(Account otherAccount, double translationSum)
        {
            if (!_client.IsApproved())
                throw new BankException($"Client isn't approved and cannot make a translation.");
            if (translationSum > Sum)
                throw new BankException($"Debit account doesn't have enough money for the transaction.");
            otherAccount.TranslationFrom(translationSum);
            Sum -= translationSum;
        }

        public override void TranslationFrom(double translationSum)
        {
            Sum += translationSum;
        }

        public override void WaitDay(uint currentDate)
        {
            if (_accumulatedSum < 0) _accumulatedSum += _settings.MonthlyPercentCommission(Sum) / 30 * Sum;
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
                if (transaction.IsAccountId(this.Id)) transactions.AddTransaction(transaction);
            return transactions;
        }

        public override bool IsClientId(uint id)
            => _client.Id == id;

        public override string Type()
            => "debit";
    }
}