using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Banks.Exceptions;
using Banks.Tools.BankSetting.BankAccountsSettings;
using Banks.Tools.ClientPart;

namespace Banks.Tools.Accounts
{
    public class AllAccounts
    {
        private List<Account> _accounts;
        private uint _currentNumberId = 1;

        public AllAccounts()
        {
            _accounts = new List<Account>();
        }

        public ImmutableList<Account> ImmutableAccounts => _accounts.ToImmutableList();

        public CreditAccount AddCreditAccount(CreditAccountSettings settings, Client client, uint dateOfCreation)
        {
            var account = new CreditAccount(settings, client, _currentNumberId, dateOfCreation);
            _accounts.Add(account);
            _currentNumberId++;
            return account;
        }

        public DebitAccount AddDebitAccount(DebitAccountSettings settings, Client client, uint dateOfCreation)
        {
            var account = new DebitAccount(settings, client, _currentNumberId, dateOfCreation);
            _accounts.Add(account);
            _currentNumberId++;
            return account;
        }

        public DepositAccount AddDepositAccount(DepositAccountSettings settings, Client client, uint dateOfCreation)
        {
            var account = new DepositAccount(settings, client, _currentNumberId, dateOfCreation);
            _accounts.Add(account);
            _currentNumberId++;
            return account;
        }

        public Account FindAccount(uint id)
            => _accounts.FirstOrDefault(account => account.Id == id);
        public void WaitDay(uint currentDay)
        {
            foreach (Account account in _accounts)
            {
                account.WaitDay(currentDay);
            }
        }
    }
}