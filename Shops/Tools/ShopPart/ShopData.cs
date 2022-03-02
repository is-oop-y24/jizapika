using System;
using System.Collections.Generic;
using Shops.Exceptions;
using Shops.Tools.CustomerPart;
using Shops.Tools.Events;
using Shops.Tools.ProductPart;

namespace Shops.Tools.ShopPart
{
    public class ShopData
    {
        private List<Shop> _shops = new List<Shop>();

        public Shop AddShop(string shopName)
        {
            var newShop = new Shop(shopName);
            this._shops.Add(newShop);
            return newShop;
        }

        public Shop FindShop(string shopname)
        {
            foreach (Shop currentShop in this._shops)
            {
                if (currentShop.Name() == shopname) return currentShop;
            }

            return null;
        }

        public Shop FindBestShopByProduct(CustomerEvent productList)
        {
            Shop bestShop = null;
            double minCost = 0;
            foreach (Shop currentShop in this._shops)
            {
                int curProductsQuantity = 0;
                foreach (EqualsProductsForShop currentProductInShop in currentShop.ProductsInfo())
                {
                    if (productList.GetPurchase().Contains(currentProductInShop))
                    {
                        curProductsQuantity++;
                    }
                }

                if (curProductsQuantity == productList.GetPurchase().Count)
                {
                    var productListCopy = (CustomerEvent)productList.Clone();
                    var shopCopy = (Shop)currentShop.Clone();
                    productListCopy.Purchase(ref shopCopy);
                    double curMinCost = productList.GetCustomer().GetBalance() - productListCopy.GetCustomer().GetBalance();
                    if (minCost == 0 || curMinCost < minCost)
                    {
                        minCost = curMinCost;
                        bestShop = currentShop;
                    }
                }
            }

            if (minCost == 0)
            {
                throw new ShoppingSystemException($"Не найдено магазинов с искомым списком товаров.");
            }

            return bestShop;
        }
    }
}