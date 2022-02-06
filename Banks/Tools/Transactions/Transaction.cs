namespace Banks.Tools.Transactions
{
    public abstract class Transaction
    {
        public double Ammount { get; }
        public abstract void Cancel();
        public abstract bool IsAccountId(uint id);
        public abstract string Type();
    }
}