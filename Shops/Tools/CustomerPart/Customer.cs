using Shops.Exceptions;

namespace Shops.Tools.CustomerPart
{
    public class Customer
    {
        private readonly string _name;
        private int _balance;

        public Customer(string customerName, int customerBalance)
        {
            this._name = customerName;
            this._balance = customerBalance;
        }

        public string Name()
        {
            return this._name;
        }

        public void CustomerSpend(int price)
        {
            if (this._balance < price)
            {
                throw new ShoppingSystemException(
                    $"У покупателя {_name} недостаточно средств для списания {price} рублей.");
            }

            this._balance -= price;
        }

        public int GetBalance()
        {
            return this._balance;
        }
    }
}