using Banks.Tools.CentralBankTools;
using Banks.Tools.Transactions;

namespace Banks.Tools.Accounts
{
    public abstract class Account
    {
        public double Sum { get; protected set; }
        public uint Id { get; protected set; }
        public abstract void MakeWithdrawal(double withdrawalSum);
        public abstract void MakeReplenishment(double replenishmentSum);
        public abstract void MakeTranslationTo(Account otherAccount, double translationSum);
        public abstract void TranslationFrom(double translationSum);

        public void CancelMakeWithdrawal(double withdrawalSum)
        {
            Sum += withdrawalSum;
        }

        public void CancelMakeReplenishment(double replenishmentSum)
        {
            Sum -= replenishmentSum;
        }

        public void CancelMakeTranslationTo(Account otherAccount, double translationSum)
        {
            otherAccount.CancelTranslationFrom(translationSum);
            Sum += translationSum;
        }

        public void CancelTranslationFrom(double translationSum)
        {
            Sum -= translationSum;
        }

        public abstract TransactionList AccountTransactions(AllTransactions allTransactions);
        public abstract void WaitDay(uint currentDate);
        public abstract bool IsClientId(uint id);
        public abstract AccountType Type();
    }
}