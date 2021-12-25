namespace Banks.Tools.Transactions
{
    public abstract class Transaction
    {
        public double Ammount { get; }
        public uint Id { get; }
        public abstract void Cancel();
        public abstract void UnCancel();
        public abstract bool IsAccountId(uint id);
        public abstract string Type();
    }
}