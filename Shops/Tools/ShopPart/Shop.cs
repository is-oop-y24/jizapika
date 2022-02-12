using System;
using System.Collections.Generic;
using Shops.Exceptions;
using Shops.Tools.ProductPart;

namespace Shops.Tools.ShopPart
{
    public class Shop : System.ICloneable
    {
        // В каждом магазине установлена своя цена на товар и есть в наличии некоторое количество единиц товара.
        private readonly List<EqualsProductsForShop> _productData;
        private readonly string _name;
        public Shop(string shopName)
        {
            this._name = shopName;
            this._productData = new List<EqualsProductsForShop>();
        }

        public void ReserveProduct(Product product)
        {
            foreach (EqualsProductsForShop currentProductInfo in this._productData)
            {
                if (currentProductInfo.GetProduct() == product)
                {
                    throw new ShoppingSystemException(
                        $"Продукт {product.Type()} уже зарезервирован.");
                }
            }

            var equalsProductsForShop = new EqualsProductsForShop(product, 0);
            this._productData.Add(equalsProductsForShop);
        }

        public string Name()
        {
            return this._name;
        }

        public List<EqualsProductsForShop> ProductsInfo()
        {
            return this._productData;
        }

        public void PriceAssignment(string productName, int newprice)
        {
            EqualsProductsForShop foundProduct = this.FindProduct(productName);
            if (foundProduct != null) foundProduct.Price = newprice;
        }

        public EqualsProductsForShop FindProduct(string productName)
        {
            foreach (EqualsProductsForShop currentProduct in _productData)
            {
                if (currentProduct.GetProduct().Type().Equals(productName))
                {
                    return currentProduct;
                }
            }

            return null;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}