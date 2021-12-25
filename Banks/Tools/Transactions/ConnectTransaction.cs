using System.Diagnostics.CodeAnalysis;
using Banks.Tools.Accounts;

namespace Banks.Tools.Transactions
{
    public class ConnectTransaction : Transaction
    {
        private Account _from;
        private Account _to;

        public ConnectTransaction(Account from, Account to, uint id, double sum)
        {
            _from = from;
            _to = to;
            Id = id;
            Ammount = sum;
            from.Sum -= sum;
            to.Sum += sum;
        }

        public new double Ammount { get; }
        public new uint Id { get; }

        public override void Cancel()
        {
            _from.Sum += Ammount;
            _to.Sum -= Ammount;
        }

        public override void UnCancel()
        {
            _from.Sum -= Ammount;
            _to.Sum += Ammount;
        }

        public override bool IsAccountId(uint id)
            => _from.Id == id || _to.Id == id;

        public override string Type()
            => "Connect";
    }
}