using System;
using System.Collections.Generic;
using HPKata.Service;
using HPKata.Service.Types;
using NUnit.Framework;

namespace HPKata.Tests.Helpers
{
    public class BasketBuilder
    {
        private readonly Basket _basket = new Basket(new DiscountProvider());

        public BasketBuilder WithBooks(int quantity, double price, string volume)
        {
            for (var i = 0; i < quantity; i++)
            {
                _basket.Add(new Book(price,volume));
            }

            return this;
        }

        public Basket Build() => _basket;
    }
}