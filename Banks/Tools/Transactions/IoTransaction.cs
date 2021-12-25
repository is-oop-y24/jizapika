using Banks.Tools.Accounts;

namespace Banks.Tools.Transactions
{
    public class IoTransaction : Transaction
    {
        private readonly Account _account;
        public IoTransaction(Account account, uint id, double sum)
        {
            _account = account;
            Id = id;
            Ammount = sum;
            _account.Sum += sum;
        }

        public new double Ammount { get; }
        public new uint Id { get; }
        public override void Cancel()
        {
        }

        public override void UnCancel()
        {
        }

        public override bool IsAccountId(uint id)
            => _account.Id == id;

        public override string Type()
            => "Io";
    }
}