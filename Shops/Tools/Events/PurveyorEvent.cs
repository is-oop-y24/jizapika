using System;
using System.Collections.Generic;
using Shops.Exceptions;
using Shops.Tools.ProductPart;
using Shops.Tools.ShopPart;

namespace Shops.Tools.Events
{
    public class PurveyorEvent
    {
        private readonly List<EqualsProductsForShop> _supply;

        public PurveyorEvent(List<EqualsProductsForShop> supply)
        {
            this._supply = supply;
        }

        public void AddProductsToSupply(List<EqualsProductsForShop> newSupply)
        {
            foreach (EqualsProductsForShop curProduct in this._supply)
            {
                foreach (EqualsProductsForShop curNewProduct in newSupply)
                {
                    if (curProduct.GetProduct() == curNewProduct.GetProduct())
                    {
                        throw new ShoppingSystemException(
                            $"Необходимо убрать из списка увеличения поставки продукт: {curProduct.GetProduct().Type()}");
                    }
                }
            }

            this._supply.AddRange(newSupply);
        }

        public void Delivery(ref Shop shop)
        {
            foreach (EqualsProductsForShop deliveryProduct in this._supply)
            {
                bool needToAdd = true;
                foreach (EqualsProductsForShop curShopProduct in shop.ProductsInfo())
                {
                    if (curShopProduct.GetProduct().Equals(deliveryProduct.GetProduct())) needToAdd = false;
                }

                if (needToAdd)
                    throw new ShoppingSystemException($"Продукт {deliveryProduct.GetProduct().Type()} не зарезервирован в магазине {shop.Name()}, поэтому не может быть поставлен.");
            }

            foreach (EqualsProductsForShop deliveryProduct in this._supply)
            {
                foreach (EqualsProductsForShop curShopProduct in shop.ProductsInfo())
                {
                    if (curShopProduct.GetProduct().Equals(deliveryProduct.GetProduct()))
                    {
                        curShopProduct.ProductQuantityChangedBy(Convert.ToInt32(deliveryProduct.GetQuantity()));
                    }
                }
            }
        }
    }
}