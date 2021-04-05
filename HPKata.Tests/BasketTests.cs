using FluentAssertions;
using HPKata.Service;
using HPKata.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;

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
                                 .AddFiveBookBundle()
                                 .AddFiveBookBundle()
                                 .AddFourBookBundle()
                                 .AddTwoBookBundle()
                                 .AddSingleBook()
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
            var basket = new Basket(new QuantityDiscountProvider());
            Action act = () =>
            {
                basket.Add(null);
            };

            act.Should().Throw<ArgumentNullException>()
               .And.ParamName.Should().Be("book");
        }



        [TestCase(8.0)]
        [TestCase(10.0)]
        public void IndividualBookShouldReturnItsIndividualPrice(decimal bookPrice)
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, bookPrice, "Volume 1")
                         .Build();

            var result = basket.CalculateTotal();

            result.Should().Be(bookPrice);
        }
        
        [TestCase(16,2)]
        [TestCase(24,3)]
        public void MultipleIdenticalBooksShouldReturnTheCombinePrice(decimal expected, int bookQty)
        {
            var basket = new BasketBuilder()
                         .WithBooks(bookQty, 8, "Volume 1")
                         .Build();


            var result = basket.CalculateTotal();

            result.Should().Be(expected);
        }

        [Test]
        public void TwoBooksFromSeriesShouldReturnFivePercentDiscount()
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, 8, "Volume 1")
                         .WithBooks(1, 8, "Volume 2")
                         .Build();
            
            var result = basket.CalculateTotal();

            result.Should().Be(15.2m);
        }

        [Test]
        public void ThreeBooksFromSeriesShouldReturnTenPercentDiscount()
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, 8, "Volume 1")
                         .WithBooks(1, 8, "Volume 2")
                         .WithBooks(1, 8, "Volume 3")
                         .Build();
            
            var result = basket.CalculateTotal();

            result.Should().Be(21.6m);
        }

        [Test]
        public void FourBooksFromSeriesShouldReturnTwentyPercentDiscount()
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, 8, "Volume 1")
                         .WithBooks(1, 8, "Volume 2")
                         .WithBooks(1, 8, "Volume 3")
                         .WithBooks(1, 8, "Volume 4")
                         .Build();
            
            var result = basket.CalculateTotal();

            result.Should().Be(25.6m);
        }

        [Test]
        public void FiveBooksFromSeriesShouldReturnTwentyFivePercentDiscount()
        {
            var basket = new BasketBuilder()
                         .WithBooks(1, 8, "Volume 1")
                         .WithBooks(1, 8, "Volume 2")
                         .WithBooks(1, 8, "Volume 3")
                         .WithBooks(1, 8, "Volume 4")
                         .WithBooks(1, 8, "Volume 5")
                         .Build();
            
            var result = basket.CalculateTotal();

            result.Should().Be(30);
        }

        [Test]
        public void BooksOutsideOfBundleShouldBeAtFullPrice()
        {
            var basket = new BasketBuilder()
                         .WithBooks(2, 8, "Volume 1") //first Book in bundle, one book at full cost.
                         .WithBooks(1, 8, "Volume 2") //second book in bundle.
                         .Build();

            var result = basket.CalculateTotal();

            result.Should().Be(23.20m);
        }

        [Test]
        [TestCase(4,0,0,0,0,32)]
        [TestCase(2,2,2,1,1,51.20)]
        [TestCase(4,2,4,1,1,81.60)]
        [TestCase(5,4,3,2,2,104.40)]
        [TestCase(5,4,3,2,3,108.80)]
        public void ShouldReturnTheBiggestDiscountAvailable(int vol1,int vol2,int vol3,int vol4, int vol5, decimal expected)
        {
            var basket = new BasketBuilder()
                         .WithBooks(vol1, 8, "Volume 1")
                         .WithBooks(vol2, 8, "Volume 2")
                         .WithBooks(vol3, 8, "Volume 3")
                         .WithBooks(vol4, 8, "Volume 4")
                         .WithBooks(vol5, 8, "Volume 5")
                         .Build();

            var result = basket.CalculateTotal();

            result.Should().Be(expected);
        }
    }
}