using Shops.Tools.CustomerPart;
using Shops.Tools.Events;
using Shops.Tools.ProductPart;
using Shops.Tools.ShopPart;

namespace Shops.Services
{
    public interface IShopManager
    {
        public Shop ReserveShop(string name);
        public Customer ReserveCustomer(string name, int balance);
        public void MakePurchase(string shopName, CustomerEvent purchase);
        public void MakeDelivery(string shopName, PurveyorEvent delivery);
        public Shop FindBestShopByProduct(CustomerEvent productList);
    }
}