namespace Banks.Tools.Transactions
{
    public abstract class Transaction
    {
        public double Ammount { get; }
        public uint Id { get; }
        public abstract void MakeIt();
        public abstract void CancelIt();
        public abstract bool IsAccountId(uint id);
        public abstract string Type();
    }
}