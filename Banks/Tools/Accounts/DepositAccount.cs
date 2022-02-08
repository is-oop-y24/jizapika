using Banks.Exceptions;
using Banks.Tools.BankSetting.BankAccountsSettings;
using Banks.Tools.ClientPart;
using Banks.Tools.Transactions;

namespace Banks.Tools.Accounts
{
    public class DepositAccount : Account
    {
        private Client _client;
        private DepositAccountSettings _settings;
        private uint _dateOfCreation;
        private double _accumulatedSum;
        private uint _currentDate;

        public DepositAccount(DepositAccountSettings settings, Client client, uint id, uint dateOfCreation)
        {
            _client = client;
            _settings = settings;
            _dateOfCreation = dateOfCreation;
            _accumulatedSum = 0;
            _currentDate = dateOfCreation;
            Sum = 0;
            Id = id;
        }

        public new double Sum { get; internal set; }
        public new uint Id { get; }

        public override void MakeWithdrawal(double withdrawalSum)
        {
            if (!_client.IsApproved())
                throw new BankException($"Client isn't approved and cannot make a withdrawal.");
            if (_currentDate < _dateOfCreation + _settings.DaysDuration)
                throw new BankException($"The deposit period isn't over yet");
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
            if (_currentDate < _dateOfCreation + _settings.DaysDuration)
                throw new BankException($"The deposit period isn't over yet");
            otherAccount.TranslationFrom(translationSum);
            Sum -= translationSum;
        }

        public override void TranslationFrom(double translationSum)
        {
            Sum += translationSum;
        }

        public override void WaitDay(uint currentDate)
        {
            if (currentDate > _dateOfCreation + _settings.DaysDuration) return;
            _accumulatedSum += _settings.MonthlyPercentCommission(Sum) / 30 * Sum;
            if ((currentDate == _dateOfCreation + _settings.DaysDuration) || (currentDate - _dateOfCreation) % 30 == 0)
            {
                Sum += _accumulatedSum;
                _accumulatedSum = 0;
            }

            _currentDate = currentDate;
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

        public override AccountType Type()
            => AccountType.Deposit;
    }
}