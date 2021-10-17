using System;
using System.Collections.Generic;
using Shops.Services;
using NUnit.Framework;
using Shops.Exceptions;
using Shops.Tools.CustomerPart;
using Shops.Tools.Events;
using Shops.Tools.ProductPart;
using Shops.Tools.ShopPart;

namespace Shops.Tests
{
    public class Tests
    {
        private static readonly object[] PurveyorAndPurchaseTest =
        {
            new object[]
            {
                new PurveyorEvent(
                    new List<EqualsProductsForShop>
                    {
                        new EqualsProductsForShop(new Product("banana"), 8),
                        new EqualsProductsForShop(new Product("potato"), 8)
                    }
                ),
                new CustomerEvent(
                    new List<EqualsProductsForShop>
                    {
                        new EqualsProductsForShop(new Product("banana"), 3),
                        new EqualsProductsForShop(new Product("potato"), 2)
                    },
                    new Customer("Lev", 38058)
                ),
                new List<(Product product, int cost)>
                {
                    (new Product("banana"), 8700),
                    (new Product("potato"), 6600)
                }
            },
            new object[]
            {
                new PurveyorEvent(
                    new List<EqualsProductsForShop>
                    {
                        new EqualsProductsForShop(new Product("bananas"), 8),
                        new EqualsProductsForShop(new Product("potatoes"), 8)
                    }
                ),
                new CustomerEvent(
                    new List<EqualsProductsForShop>
                    {
                        new EqualsProductsForShop(new Product("banana"), 1),
                        new EqualsProductsForShop(new Product("potatoes"), 2)
                    },
                    new Customer("Lev", 138058)
                ),
                new List<(Product product, int cost)>
                {
                    (new Product("bananas"), 8700),
                    (new Product("potatoes"), 6600)
                }
            },
            new object[]
            {
                new PurveyorEvent(
                    new List<EqualsProductsForShop>
                    {
                        new EqualsProductsForShop(new Product("banana"), 8),
                        new EqualsProductsForShop(new Product("potato"), 8),
                        new EqualsProductsForShop(new Product("carrot"), 188)
                    }
                ),
                new CustomerEvent(
                    new List<EqualsProductsForShop>
                    {
                        new EqualsProductsForShop(new Product("banana"), 3),
                        new EqualsProductsForShop(new Product("potato"), 2),
                        new EqualsProductsForShop(new Product("carrot"), 18)
                    },
                    new Customer("Lev", 38058)
                ),
                new List<(Product product, int cost)>
                {
                    (new Product("banana"), 8700),
                    (new Product("potato"), 6600)
                }
            },
        };
        [TestCaseSource(nameof(PurveyorAndPurchaseTest))]
        public void PurveyorAndPurchase_ThrowException(PurveyorEvent purveyorEvent, CustomerEvent customerEvent, List<(Product product, int cost)> prices)
        {
            Assert.Catch<ShoppingSystemException>(() =>
            {
                var ourCompany = new ShopManager();
                Shop shop = ourCompany.ReserveShop("First");
                foreach ((Product product, int cost) currentProduct in prices)
                {
                    shop.ReserveProduct(currentProduct.product);
                    shop.PriceAssignment(currentProduct.product.Type(), currentProduct.cost);
                }
                purveyorEvent.Delivery(ref shop);
                customerEvent.Purchase(ref shop);
            });
        }
        
        [TestCase(70, 80)]
        [TestCase(70, 70)]
        public void ChangePrices_ChangedPrice(int firstPrice, int secondPrice)
        {
            var ourCompany = new ShopManager();
            Shop shop = ourCompany.ReserveShop("First");
            shop.ReserveProduct(new Product("bread"));
            shop.PriceAssignment("bread", firstPrice);
            Assert.AreEqual(shop.FindProduct("bread").Price,firstPrice);
            shop.PriceAssignment("bread", secondPrice);
            Assert.AreEqual(shop.FindProduct("bread").Price,secondPrice);
        }
        
        private static readonly object[] FindBestShopTest =
        {
            new object[]
            {
                new List<(List<(Product product, int cost)> productsForShops, string shopName, PurveyorEvent purveyor)>
                {
                    new ValueTuple<List<(Product product, int cost)>, string, PurveyorEvent>(
                        new List<(Product product, int cost)>()
                        {
                            new ValueTuple<Product, int>(new Product("tea"), 355),
                            new ValueTuple<Product, int>(new Product("cola"), 255),
                            new ValueTuple<Product, int>(new Product("coffee"), 455),
                        },
                        "Diksi",
                        new PurveyorEvent(
                            new List<EqualsProductsForShop>
                            {
                                new EqualsProductsForShop(new Product("tea"), 888),
                                new EqualsProductsForShop(new Product("cola"), 888),
                                new EqualsProductsForShop(new Product("coffee"), 888),
                            }
                        )
                    ),
                    new ValueTuple<List<(Product product, int cost)>, string, PurveyorEvent>(
                        new List<(Product product, int cost)>()
                        {
                            new ValueTuple<Product, int>(new Product("tea"), 355),
                            new ValueTuple<Product, int>(new Product("cola"), 255),
                            new ValueTuple<Product, int>(new Product("pepsi"), 455),
                        },
                        "Lenta",
                        new PurveyorEvent(
                            new List<EqualsProductsForShop>
                            {
                                new EqualsProductsForShop(new Product("tea"), 888),
                                new EqualsProductsForShop(new Product("cola"), 888),
                                new EqualsProductsForShop(new Product("pepsi"), 888),
                            }
                        )
                    )
                },
                new CustomerEvent(
                    new List<EqualsProductsForShop>
                    {
                        new EqualsProductsForShop(new Product("pepsi"), 3),
                        new EqualsProductsForShop(new Product("coffee"), 2)
                    },
                    new Customer("Lev", 38058)
                )
            },
            new object[]
            {
                new List<(List<(Product product, int cost)> productsForShops, string shopName, PurveyorEvent purveyor)>
                {
                    new ValueTuple<List<(Product product, int cost)>, string, PurveyorEvent>(
                        new List<(Product product, int cost)>()
                        {
                            new ValueTuple<Product, int>(new Product("tea"), 355),
                            new ValueTuple<Product, int>(new Product("cola"), 255),
                            new ValueTuple<Product, int>(new Product("coffee"), 455),
                        },
                        "Diksi",
                        new PurveyorEvent(
                            new List<EqualsProductsForShop>
                            {
                                new EqualsProductsForShop(new Product("tea"), 888),
                                new EqualsProductsForShop(new Product("cola"), 8),
                                new EqualsProductsForShop(new Product("coffee"), 888),
                            }
                        )
                    ),
                    new ValueTuple<List<(Product product, int cost)>, string, PurveyorEvent>(
                        new List<(Product product, int cost)>()
                        {
                            new ValueTuple<Product, int>(new Product("tea"), 355),
                            new ValueTuple<Product, int>(new Product("cola"), 255),
                            new ValueTuple<Product, int>(new Product("pepsi"), 455),
                        },
                        "Lenta",
                        new PurveyorEvent(
                            new List<EqualsProductsForShop>
                            {
                                new EqualsProductsForShop(new Product("tea"), 8),
                                new EqualsProductsForShop(new Product("cola"), 888),
                                new EqualsProductsForShop(new Product("pepsi"), 888),
                            }
                        )
                    )
                },
                new CustomerEvent(
                    new List<EqualsProductsForShop>
                    {
                        new EqualsProductsForShop(new Product("tea"), 33),
                        new EqualsProductsForShop(new Product("cola"), 22)
                    },
                    new Customer("Lev", 380580)
                )
            }
        };
        [TestCaseSource(nameof(FindBestShopTest))]
        public void FindBestShop_ThrowException(List<(List<(Product product, int cost)> productsForShops, string shopName, PurveyorEvent purveyor)> shops, CustomerEvent productList)
        {
            Assert.Catch<ShoppingSystemException>(() =>
            {
                var ourCompany = new ShopManager();
                foreach ((List<(Product product, int cost)> productsForShops, string shopName, PurveyorEvent purveyor) infoForShop in shops)
                {
                    Shop shop = ourCompany.ReserveShop(infoForShop.shopName);
                    foreach ((Product product, int cost) currentProduct in infoForShop.productsForShops)
                    {
                        shop.ReserveProduct(currentProduct.product);
                        shop.PriceAssignment(currentProduct.product.Type(), currentProduct.cost);
                    }
                    infoForShop.purveyor.Delivery(ref shop);
                }

                ourCompany.FindBestShopByProduct(productList);
            });
        }
        
        [TestCase(70000, 80, 150u, 100u, true)]
        [TestCase(7000, 100, 150u, 100u, false)]
        public void CheckPurchase_ThrowExceptionOrGoodPurchase(int moneyBefore, int productPrice, uint productCount, uint productToBuyCount, bool t)
        {
            var customer = new Customer("Lev", moneyBefore);
            var shopManager = new ShopManager();
            Shop shop = shopManager.ReserveShop("shop name");
            var product = new Product("product name");
            shop.ReserveProduct(product);
            var productForShop = new EqualsProductsForShop(product, productCount);
            var productForBuy = new EqualsProductsForShop(product, productToBuyCount);
            var supply = new List<EqualsProductsForShop>();
            supply.Add(productForShop);
            var productList = new List<EqualsProductsForShop>();
            productList.Add(productForBuy);
            var purveyorEvent = new PurveyorEvent(supply);
            var customerEvent = new CustomerEvent(productList, customer);
            if (t)
            {
                purveyorEvent.Delivery(ref shop);
                shop.PriceAssignment(product.Type(), productPrice);
                customerEvent.Purchase(ref shop);
                Assert.AreEqual(moneyBefore - productPrice * productToBuyCount, customer.GetBalance());
                Assert.AreEqual(productCount - productToBuyCount, shop.FindProduct(product.Type()).GetQuantity());
            }
            else
            {
                Assert.Catch<ShoppingSystemException>(() =>
                {
                    purveyorEvent.Delivery(ref shop);
                    shop.PriceAssignment(product.Type(), productPrice);
                    customerEvent.Purchase(ref shop);
                });
            }
        }
    }
}