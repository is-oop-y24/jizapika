using System.Collections.Generic;
using System.Net.Http.Headers;
using Shops.Tools.CustomerPart;
using Shops.Tools.Events;
using Shops.Tools.ProductPart;
using Shops.Tools.ShopPart;

namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private ShopData _shops = new ShopData();
        private CustomerData _customers = new CustomerData();

        public Shop ReserveShop(string name)
        {
            return this._shops.AddShop(name);
        }

        public Customer ReserveCustomer(string name, int balance)
        {
            return this._customers.AddCustomer(name, balance);
        }

        public void MakePurchase(string shopName, CustomerEvent purchase)
        {
            Shop shop = this._shops.FindShop(shopName);
            purchase.Purchase(ref shop);
        }

        public void MakeDelivery(string shopName, PurveyorEvent delivery)
        {
            Shop shop = this._shops.FindShop(shopName);
            delivery.Delivery(ref shop);
        }

        public Shop FindBestShopByProduct(CustomerEvent productList)
        {
            return this._shops.FindBestShopByProduct(productList);
        }
    }
}