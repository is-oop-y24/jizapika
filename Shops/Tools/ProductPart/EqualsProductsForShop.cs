using System;
using Shops.Exceptions;

namespace Shops.Tools.ProductPart
{
    public class EqualsProductsForShop
    {
        private readonly Product _product;
        private uint _quantity;
        private int _price = 0;

        public EqualsProductsForShop(Product product, uint quantity)
        {
            this._product = product;
            this._quantity = quantity;
        }

        public int Price
        {
            get => this._price;
            set
            {
                if (value > 0) this._price = value;
                else throw new ShoppingSystemException("Цена товара должна быть положительной.");
            }
        }

        public void ProductQuantityChangedBy(int change)
        {
            try
            {
                int currentQuantity = Convert.ToInt32(this._quantity);
                currentQuantity += change;
                this._quantity = Convert.ToUInt32(currentQuantity);
            }
            catch (FormatException)
            {
                throw new ShoppingSystemException($"Количество товара \"{this._product.Type()}\" не может измениться на {change}.");
            }
        }

        public Product GetProduct()
        {
            return _product;
        }

        public uint GetQuantity()
        {
            return _quantity;
        }

        public int SummaryPrice()
        {
            if (_price == 0) throw new ShoppingSystemException($"Цена на товар {_product.Type()} не установлена.");
            return _price * (int)_quantity;
        }
    }
}