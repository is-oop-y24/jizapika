using System.Collections.Generic;
using System.Collections.Immutable;

namespace Banks.Tools.Accounts
{
    public class AccountList
    {
        private List<Account> _accounts;
        public AccountList()
        {
            _accounts = new List<Account>();
        }

        public ImmutableList<Account> ImmutableAccounts => _accounts.ToImmutableList();

        public void AddAccount(Account account)
        {
            if (account != null)
                _accounts.Add(account);
        }
    }
}