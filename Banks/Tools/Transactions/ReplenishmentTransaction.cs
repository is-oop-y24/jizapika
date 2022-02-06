using Banks.Tools.Accounts;

namespace Banks.Tools.Transactions
{
    public class ReplenishmentTransaction : Transaction
    {
        private Account _to;
        private bool _isComleted;
        private uint _id;

        public ReplenishmentTransaction(Account to, uint id, double sum)
        {
            _isComleted = false;
            _to = to;
            _id = id;
            Ammount = sum;
        }

        public new double Ammount { get; set; }

        public override void MakeIt()
        {
            _isComleted = true;
            _to.Sum += Ammount;
        }

        public override void CancelIt()
        {
            _to.Sum -= Ammount;
        }

        public override bool IsAccountId(uint id)
            => _to.Id == id;

        public override string Type()
            => "Connect";
    }
}