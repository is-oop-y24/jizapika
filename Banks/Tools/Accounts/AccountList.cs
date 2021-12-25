using System.Collections.Generic;

namespace Banks.Tools.Accounts
{
    public class AccountList
    {
        public AccountList()
        {
            Accounts = new List<Account>();
        }

        public List<Account> Accounts { get; }

        public void AddAccount(Account account)
        {
            Accounts.Add(account);
        }
    }
}