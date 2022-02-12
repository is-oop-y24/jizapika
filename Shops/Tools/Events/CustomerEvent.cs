using System;
using System.Collections.Generic;
using Shops.Exceptions;
using Shops.Tools.CustomerPart;
using Shops.Tools.ProductPart;
using Shops.Tools.ShopPart;

namespace Shops.Tools.Events
{
    public class CustomerEvent : System.ICloneable
    {
        private readonly List<EqualsProductsForShop> _purchase;
        private readonly Customer _customer;

        public CustomerEvent(List<EqualsProductsForShop> purchase, Customer customer)
        {
            this._purchase = purchase;
            this._customer = customer;
        }

        public void Purchase(ref Shop shop)
        {
            int cost = 0;
            foreach (EqualsProductsForShop curBuyProduct in _purchase)
            {
                bool noProduct = true;
                foreach (EqualsProductsForShop curShopProduct in shop.ProductsInfo())
                {
                    if (curShopProduct.GetProduct().Equals(curBuyProduct.GetProduct()))
                    {
                        cost += curShopProduct.SummaryPrice();
                        noProduct = false;
                        if (curBuyProduct.GetQuantity() > curShopProduct.GetQuantity())
                        {
                            throw new ShoppingSystemException(
                                $"Кол-во товара {curBuyProduct.GetProduct().Type()} в магазине {shop.Name()} меньше, чем {_customer.Name()} хочет купить.");
                        }
                    }
                }

                if (noProduct)
                {
                    throw new ShoppingSystemException(
                        $"Продукта {curBuyProduct.GetProduct().Type()} нет в магазине {shop.Name()}");
                }
            }

            foreach (EqualsProductsForShop curBuyProduct in _purchase)
            {
                foreach (EqualsProductsForShop curShopProduct in shop.ProductsInfo())
                {
                    if (curShopProduct.GetProduct().Equals(curBuyProduct.GetProduct()))
                    {
                        curShopProduct.ProductQuantityChangedBy(-Convert.ToInt32(curBuyProduct.GetQuantity()));
                        cost -= curShopProduct.SummaryPrice();
                    }
                }
            }

            this._customer.CustomerSpend(cost);
        }

        public List<EqualsProductsForShop> GetPurchase()
        {
            return this._purchase;
        }

        public Customer GetCustomer()
        {
            return this._customer;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}