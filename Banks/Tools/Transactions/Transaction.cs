namespace Banks.Tools.Transactions
{
    public abstract class Transaction
    {
        public double Ammount { get; protected set; }
        public uint Id { get; protected set; }
        public abstract void MakeIt();
        public abstract void CancelIt();
        public abstract bool IsAccountId(uint id);
        public abstract TransactionType Type();
    }
}