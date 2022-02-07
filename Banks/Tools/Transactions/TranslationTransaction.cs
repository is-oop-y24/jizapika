using Banks.Tools.Accounts;

namespace Banks.Tools.Transactions
{
    public class TranslationTransaction : Transaction
    {
        private Account _from;
        private Account _to;
        private bool _isComleted;

        public TranslationTransaction(Account from, Account to, uint id, double sum)
        {
            _isComleted = false;
            _to = to;
            _from = from;
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
                _from.MakeTranslationTo(_to, Ammount);
            }
        }

        public override void CancelIt()
        {
            if (_isComleted)
            {
                _isComleted = false;
                _from.CancelMakeTranslationTo(_to, Ammount);
            }
        }

        public override bool IsAccountId(uint id)
            => _to.Id == id;

        public override string Type()
            => "Translation";
    }
}