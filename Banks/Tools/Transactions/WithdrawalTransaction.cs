using Banks.Tools.Accounts;

namespace Banks.Tools.Transactions
{
    public class WithdrawalTransaction : Transaction
    {
        private Account _from;
        private bool _isComleted;

        public WithdrawalTransaction(Account from, uint id, double sum)
        {
            _isComleted = false;
            _from = from;
            Id = id;
            Ammount = sum;
        }

        public override void MakeIt()
        {
            if (_isComleted) return;
            _isComleted = true;
            _from.MakeWithdrawal(Ammount);
        }

        public override void CancelIt()
        {
            if (!_isComleted) return;
            _isComleted = false;
            _from.CancelMakeWithdrawal(Ammount);
        }

        public override bool IsAccountId(uint id)
            => _from.Id == id;

        public override TransactionType Type()
            => TransactionType.Withdrawal;
    }
}