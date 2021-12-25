using Banks.Tools.Transactions;

namespace Banks.Tools.Accounts
{
    public abstract class Account
    {
        public double Sum { get; internal set; }
        public uint Id { get; }
        public abstract IoTransaction Withdrawal(double withdrawalSum, uint transactionId);
        public abstract IoTransaction Replenishment(double replenishmentSum, uint transactionId);
        public abstract ConnectTransaction TranslationTo(Account otherAccount, double translationSum, uint transactionId);
        public abstract TransactionList AccountTransactions(AllTransactions allTransactions);
        public abstract void WaitDay(uint currentDate);
        public abstract bool IsClientId(uint id);
        public abstract string Type();
    }
}