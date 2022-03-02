using System;
using Shops.Exceptions;
using Shops.Tools.ProductPart;

namespace Shops.Tools.ProductPart
{
    public class Product
    {
        private readonly string _type;

        public Product(string type)
        {
            this._type = type;
        }

        public string Type()
        {
            return this._type;
        }
    }
}