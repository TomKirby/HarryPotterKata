using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using HPKata.Service;
using HPKata.Service.Types;
using HPKata.Tests.Helpers;
using NUnit.Framework;

namespace HPKata.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class BasketTests
    {
        [Test]
        public void ShouldRequireBookToBeAdded()
        {
            var basket = new Basket();
            Action act = () =>
            {
                basket.Add(null);
            };

            act.Should().Throw<ArgumentNullException>()
               .And.ParamName.Should().Be("book");
        }

        [TestCase(8.0)]
        [TestCase(10.0)]
        public void IndividualBookShouldReturnItsIndividualPrice(double bookPrice)
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, bookPrice, "Volume 1")
                         .Build();

            var result = basket.CheckOut();

            result.Should().Be(bookPrice);
        }
        
        [TestCase(16,2)]
        [TestCase(24,3)]
        public void MultipleIdenticalBooksShouldReturnTheCombinePrice(double expected, int bookQty)
        {
            var basket = new BasketBuilder()
                         .WithBooks(bookQty, 8.0, "Volume 1")
                         .Build();


            var result = basket.CheckOut();

            result.Should().Be(expected);
        }

        [Test]
        public void TwoBooksFromSeriesShouldReturnFivePercentDiscount()
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, 8.0, "Volume 1")
                         .WithBooks(1, 8.0, "Volume 2")
                         .Build();
            
            var result = basket.CheckOut();

            result.Should().Be(15.2);
        }

        [Test]
        public void ThreeBooksFromSeriesShouldReturnTenPercentDiscount()
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, 8.0, "Volume 1")
                         .WithBooks(1, 8.0, "Volume 2")
                         .WithBooks(1, 8.0, "Volume 3")
                         .Build();
            
            var result = basket.CheckOut();

            result.Should().Be(21.06);
        }

        [Test]
        public void FourBooksFromSeriesShouldReturnFifteenPercentDiscount()
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, 8.0, "Volume 1")
                         .WithBooks(1, 8.0, "Volume 2")
                         .WithBooks(1, 8.0, "Volume 3")
                         .WithBooks(1, 8.0, "Volume 4")
                         .Build();
            
            var result = basket.CheckOut();

            result.Should().Be(27.2); //TODO: CHECK PERCENTAGES AGAINST REQUIREMENTS! THINK THIS IS 20% NOT 15%!!
        }

        [Test]
        public void FiveBooksFromSeriesShouldReturnFifteenPercentDiscount()
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, 8.0, "Volume 1")
                         .WithBooks(1, 8.0, "Volume 2")
                         .WithBooks(1, 8.0, "Volume 3")
                         .WithBooks(1, 8.0, "Volume 4")
                         .Build();
            
            var result = basket.CheckOut();

            result.Should().Be(27.2);
        }
    }
}