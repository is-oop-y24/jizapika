using Banks.Tools.Accounts;

namespace Banks.Tools.Transactions
{
    public class ReplenishmentTransaction : Transaction
    {
        private Account _to;
        private bool _isComleted;

        public ReplenishmentTransaction(Account to, uint id, double sum)
        {
            _isComleted = false;
            _to = to;
            Id = id;
            Ammount = sum;
        }

        public new uint Id { get; }

        public new double Ammount { get; }

        public override void MakeIt()
        {
            if (!_isComleted)
            {
                _isComleted = true;
                _to.MakeReplenishment(Ammount);
            }
        }

        public override void CancelIt()
        {
            if (_isComleted)
            {
                _isComleted = false;
                _to.CancelMakeReplenishment(Ammount);
            }
        }

        public override bool IsAccountId(uint id)
            => _to.Id == id;

        public override string Type()
            => "Replenishment";
    }
}