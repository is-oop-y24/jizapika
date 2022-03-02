namespace Banks.Tools.Accounts
{
    /// <summary> AccountType. </summary>
    public enum AccountType : uint
    {
        /// <summary> CreditAccount </summary>
        Credit = 1,

        /// <summary> DebitAccount </summary>
        Debit = 2,

        /// <summary> DepositAccount </summary>
        Deposit = 3,
    }
}