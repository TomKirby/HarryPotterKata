using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Channels;
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
        [Explicit]
        public void Helper_CalculateTotalPrice()
        {
            var calculatedCost = new BundleCalculatorHelper(8)
                       .AddFourBookBundle()
                       .AddFourBookBundle()
                       .AddFourBookBundle()
                       .AddFourBookBundle()
                       .AddFourBookBundle()
                       .AddFourBookBundle()
                       .AddFourBookBundle()
                       .AddFourBookBundle()
                       .Calculate;

            Console.WriteLine(calculatedCost);
        }

        [Test]
        public void ShouldThrowExceptionIfDiscountProviderDependencyIsMissing()
        {
            Action act = () => { new Basket(null); };
            act.Should().Throw<ArgumentNullException>()
               .And.ParamName.Should().Be("discountProvider");
        }


        [Test]
        public void ShouldRequireBookToBeAdded()
        {
            var basket = new Basket(new DiscountProvider());
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

            var result = basket.CalculateTotal();

            result.Should().Be(bookPrice);
        }
        
        [TestCase(16,2)]
        [TestCase(24,3)]
        public void MultipleIdenticalBooksShouldReturnTheCombinePrice(double expected, int bookQty)
        {
            var basket = new BasketBuilder()
                         .WithBooks(bookQty, 8.0, "Volume 1")
                         .Build();


            var result = basket.CalculateTotal();

            result.Should().Be(expected);
        }

        [Test]
        public void TwoBooksFromSeriesShouldReturnFivePercentDiscount()
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, 8.0, "Volume 1")
                         .WithBooks(1, 8.0, "Volume 2")
                         .Build();
            
            var result = basket.CalculateTotal();

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
            
            var result = basket.CalculateTotal();

            result.Should().Be(21.6);
        }

        [Test]
        public void FourBooksFromSeriesShouldReturnTwentyPercentDiscount()
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, 8.0, "Volume 1")
                         .WithBooks(1, 8.0, "Volume 2")
                         .WithBooks(1, 8.0, "Volume 3")
                         .WithBooks(1, 8.0, "Volume 4")
                         .Build();
            
            var result = basket.CalculateTotal();

            result.Should().Be(25.6);
        }

        [Test]
        public void FiveBooksFromSeriesShouldReturnTwentyFivePercentDiscount()
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, 8.0, "Volume 1")
                         .WithBooks(1, 8.0, "Volume 2")
                         .WithBooks(1, 8.0, "Volume 3")
                         .WithBooks(1, 8.0, "Volume 4")
                         .WithBooks(1, 8.0, "Volume 5")
                         .Build();
            
            var result = basket.CalculateTotal();

            result.Should().Be(30);
        }

        [Test]
        public void BooksOutsideOfBundleShouldBeAtFullPrice()
        {
            var basket = new BasketBuilder()
                         .WithBooks(2, 8.0, "Volume 1") //first Book in bundle, one book at full cost.
                         .WithBooks(1, 8.0, "Volume 2") //second book in bundle.
                         .Build();

            var result = basket.CalculateTotal();

            result.Should().Be(23.20);
        }

        [Test]
        public void ShouldReturnTheBiggestDiscountAvailable()
        {
            var basket = new BasketBuilder()
                         .WithBooks(2, 8.0, "Volume 1")
                         .WithBooks(2, 8.0, "Volume 2")
                         .WithBooks(2, 8.0, "Volume 3")
                         .WithBooks(1, 8.0, "Volume 4")
                         .WithBooks(1, 8.0, "Volume 5")
                         .Build();

            var result = basket.CalculateTotal();

            result.Should().Be(51.20);
        }
    }
}