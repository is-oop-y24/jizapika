namespace Banks.Tools.Transactions
{
    /// <summary> TransactionType. </summary>
    public enum TransactionType : uint
    {
        /// <summary> ReplenishmentTransaction </summary>
        Replenishment = 1,

        /// <summary> TranslationTransaction </summary>
        Translation = 2,

        /// <summary> WithdrawalTransaction </summary>
        Withdrawal = 3,
    }
}