using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Banks.Exceptions;
using Banks.Tools.Accounts;
using Banks.Tools.BankSetting.BankAccountsSettings;
using Banks.Tools.ClientPart;

namespace Banks.Tools.CentralBankTools
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

        public uint AddCreditAccount_ReturnID(CreditAccountSettings settings, Client client, uint dateOfCreation)
        {
            CreditAccount account = new (settings, client, _currentNumberId, dateOfCreation);
            _accounts.Add(account);
            _currentNumberId++;
            return account.Id;
        }

        public uint AddDebitAccount_ReturnID(DebitAccountSettings settings, Client client, uint dateOfCreation)
        {
            DebitAccount account = new (settings, client, _currentNumberId, dateOfCreation);
            _accounts.Add(account);
            _currentNumberId++;
            return account.Id;
        }

        public uint AddDepositAccount_ReturnID(DepositAccountSettings settings, Client client, uint dateOfCreation)
        {
            DepositAccount account = new (settings, client, _currentNumberId, dateOfCreation);
            _accounts.Add(account);
            _currentNumberId++;
            return account.Id;
        }

        public Account FindAccount(uint id)
        {
            if (!IsCorrectAccountId(id)) throw new BankException("Account doesn't exist.");
            return _accounts.FirstOrDefault(account => account.Id == id);
        }

        public void WaitDay(uint currentDay)
        {
            foreach (Account account in _accounts)
            {
                account.WaitDay(currentDay);
            }
        }

        public bool IsCorrectAccountId(uint accountId)
            => _accounts.Any(account => account.Id == accountId);
    }
}