using System.Collections.Generic;

namespace Shops.Tools.CustomerPart
{
    public class CustomerData
    {
        private readonly List<Customer> _customers = new List<Customer>();

        public Customer AddCustomer(string customerName, int customerBalance)
        {
            var newCustomer = new Customer(customerName, customerBalance);
            this._customers.Add(newCustomer);
            return newCustomer;
        }

        public Customer FindCustomer(string customername)
        {
            foreach (Customer currentCustomer in this._customers)
            {
                if (currentCustomer.Name() == customername) return currentCustomer;
            }

            return null;
        }
    }
}